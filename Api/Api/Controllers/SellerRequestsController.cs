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

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerRequestsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public SellerRequestsController(AppDbContext context,IUserService userService,IFileService fileService)
        {
            _context = context;
            _userService = userService;
            _fileService = fileService;
        }

        // GET: api/SellerRequests
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SellerRequest>>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        // GET: api/SellerRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SellerRequest>> GetSellerRequest(int id)
        {
            var sellerRequest = await _context.Requests.FindAsync(id);

            if (sellerRequest == null)
            {
                return NotFound();
            }

            return sellerRequest;
        }

        // PUT: api/SellerRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSellerRequest(int id, SellerRequest sellerRequest)
        {
            if (id != sellerRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(sellerRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SellerRequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SellerRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

            var idPhotoPath = await _fileService.SaveFileAsync(new UploadedFile.FileBuilder().File(sellerRequestDto.Id).AllowImg().MakePrivate().Build());

            _context.Requests.Add(sellerRequestDto.ToEntity(user.Id, idPhotoPath));
            await _context.SaveChangesAsync();

            return Ok("Request sent successfully");
        }

        // DELETE: api/SellerRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSellerRequest(int id)
        {
            var sellerRequest = await _context.Requests.FindAsync(id);
            if (sellerRequest == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(sellerRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SellerRequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
