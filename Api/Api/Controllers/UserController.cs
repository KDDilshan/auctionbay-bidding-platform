using Api.Data;
using Api.Dtos;
using Api.Entities;
using Api.Mapping;
using Api.Models.Email;
using Api.Services.EmailService;
using Api.Services.JwtService;
using Api.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;
        private readonly AppDbContext _context;

        public UserController(UserManager<AppUser> userManager, IEmailService emailService,IJwtService jwtService,IUserService userService,AppDbContext appDbContext)
        {
            _userManager = userManager;
            _emailService = emailService;
            _jwtService = jwtService;
            _userService = userService;
            _context = appDbContext;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDto registerDto) 
        {
            if (!ModelState.IsValid)return BadRequest(ModelState);

            var user = registerDto.ToEntity();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "User");

            _emailService.Send(new RegistrationEmail(user.ToDto()));

            return Ok("User created successfully");
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto) 
        {
            if (!ModelState.IsValid)return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
            {
                return Unauthorized(new AuthResponseDto
                {
                    Message = "User not found with this email",
                });
            }

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result)
            {
                return Unauthorized(new AuthResponseDto
                {
                    Message = "Invalid Password."
                });
            }
            var token = _jwtService.GenerateToken(user);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Message = "Login Success.",
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUserDetail()
        {
            var user = await _userService.getCurrentUser();

            if (user is null)
            {
                return NotFound(new AuthResponseDto
                {
                    Message = "User not found"
                });
            }

            return Ok(user.ToDto());
        }

        [Authorize]
        [HttpPut("UpdateUserNames")]
        public async Task<ActionResult<UserDto>> ChangeUserNames([FromBody] UserNamesDto userNamesDto)
        {
            var user=await _userService.getCurrentUser();

            if (user is null)
            {
                return NotFound(new AuthResponseDto
                {
                    Message = "User not found"
                });
            }

            user.FirstName=userNamesDto.FirstName;
            user.LastName=userNamesDto.LastName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { Message = "An error occurred while updating the user", Details = ex.Message });
            }

            return Ok(user.ToDto());

        }

        [Authorize]
        [HttpPut("UpdateUserEmail")]
        public async Task<ActionResult<UserDto>> ChangeUserEmail([FromBody] UserEmailDto userEmailDto)
        {
            var user = await _userService.getCurrentUser();

            if (user is null)
            {
                return NotFound(new AuthResponseDto
                {
                    Message = "User not found"
                });
            }

            var result = await _userManager.CheckPasswordAsync(user, userEmailDto.Password);

            if (!result)
            {
                return Unauthorized(new PasswordErrorDto
                {
                    Message = "Invalid Password."
                });
            }

            user.Email = userEmailDto.Email;
            user.NormalizedEmail = userEmailDto.Email.ToUpperInvariant();
            user.NormalizedUserName= userEmailDto.Email.ToUpperInvariant();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Message = "An error occurred while Changine the Email", Details = ex.Message });
            }

            return Ok(user.ToDto());
        }

        [Authorize]
        [HttpDelete("DeleteUser")]
        public async Task<ActionResult> DeleteUser([FromBody] DeleteUserDto deleteUserDto)
        {

            var user = await _userService.getCurrentUser();

            if (user is null)
            {
                return NotFound(new AuthResponseDto
                {
                    Message = "User not found"
                });
            }

            var result = await _userManager.CheckPasswordAsync(user, deleteUserDto.Password);

            if (!result)
            {
                return Unauthorized(new PasswordErrorDto
                {
                    Message = "Invalid Password."
                });
            }

            try
            {
                _context.Users.Remove(user); 
                await _context.SaveChangesAsync();

                return Ok(new { Message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the user", Details = ex.Message });
            }

        }

        [HttpGet("all")]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();

            if (users is null)
            {
                return NotFound(new AuthResponseDto
                {
                    Message = "No users found"
                });
            }

            return Ok(users.Select(u => u.ToDto()).ToList());
        }

    }
}
