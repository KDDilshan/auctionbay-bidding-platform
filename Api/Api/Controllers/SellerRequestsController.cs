using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Api.Dtos;
using Api.Services.UserService;
using Api.Mapping;
using Api.Models;
using Api.Services.FileService;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.AspNetCore.Identity;
using Api.Services.EmailService;
using Api.Models.Email;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerRequestsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;

        public SellerRequestsController(AppDbContext context,IUserService userService,IFileService fileService, UserManager<AppUser> userManager,IEmailService emailService)
        {
            _context = context;
            _userService = userService;
            _fileService = fileService;
            _userManager = userManager;
            _emailService = emailService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SellerRequest>>> GetRequests()
        {
            var list = await _context.Requests.Include(r=>r.User).ToListAsync();
            var result = list.Select(request=> request.ToDto()).ToList();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}/image")]
        public async Task<ActionResult> GetImage(int id)
        {
            var sellerRequest = await _context.Requests.FindAsync(id);

            if (sellerRequest == null)
            {
                return NotFound();
            }

            var imagePath = Path.Combine("uploads", sellerRequest.IdPhotoPath);

            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound("Image not found");
            }

            var imageBytes = System.IO.File.ReadAllBytes(imagePath);

            var ext = Path.GetExtension(sellerRequest.IdPhotoPath);
            return File(imageBytes, "image/"+ext); 
        }

        [Authorize]
        [HttpGet("check")]
        public async Task<ActionResult<String>> CheckSellerRequest()
        {
            var user = await _userService.getCurrentUser();

            if (user is null)
            {
                return NotFound(new AuthResponseDto
                {
                    Message = "User not found"
                });
            }

            var sellerRequest = await _context.Requests.FirstOrDefaultAsync(x => x.UserId == user.Id);

            if (sellerRequest is null)
            {
                return NotFound(new AuthResponseDto
                {
                    Message = "Request not found"
                });
            }

            return Ok(new { Message=sellerRequest.Status });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> ChangeStatus(int id,RequestStatusDto dto)
        {
            try
            {
                var sellerRequest = await _context.Requests.FindAsync(id);
                var user = await _userManager.FindByIdAsync(sellerRequest.UserId);

                if (sellerRequest == null)
                {
                    return NotFound();
                }

                if (dto.Status != "Approved" && dto.Status != "Rejected")
                {
                    return BadRequest("Invalid status");
                }

                if(dto.Status == "Approved")
                {
                    await _userManager.AddToRoleAsync(user, "Seller");
                    _emailService.Send(new SellerRequestAcceptedEmail(user.FirstName,user.Email));
                }

                if(dto.Status == "Rejected")
                {
                    await _userManager.RemoveFromRoleAsync(user, "Seller");
                    _emailService.Send(new SellerRequestDeclinedEmail(user.FirstName, user.Email));
                }

                sellerRequest.Status = dto.Status;
                await _context.SaveChangesAsync();

                return Ok("status updated");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> PostSellerRequest(SellerRequestDto sellerRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userService.getCurrentUser();

            if (user is null)
            {
                return NotFound(new AuthResponseDto
                {
                    Message = "User not found"
                });
            }

            try
            {
                var idPhotoPath = await _fileService.SaveFileAsync(new UploadedFile.FileBuilder().File(sellerRequestDto.Id).AllowImg().MakePrivate().Build());

                _context.Requests.Add(sellerRequestDto.ToEntity(user.Id, idPhotoPath));
                await _context.SaveChangesAsync();

                _emailService.Send(new SellerRequestPlacedEmail(user.FirstName, user.Email));

                return Ok("Request sent successfully");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private bool SellerRequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
