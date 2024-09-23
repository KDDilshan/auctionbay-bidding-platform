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

        public UserController(UserManager<AppUser> userManager, IEmailService emailService,IJwtService jwtService,IUserService userService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDto registerDto) 
        {
            if (!ModelState.IsValid)return BadRequest(ModelState);

            var user = registerDto.ToEntity();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "Buyer");

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
    }
}
