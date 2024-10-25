using Api.Data;
using Api.Entities;
using Api.Services.EmailService;
using Api.Services.FileService;
using Api.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;

        public SummaryController(AppDbContext context, IUserService userService, UserManager<AppUser> userManager, IEmailService emailService)
        {
            _context = context;
            _userService = userService;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpGet("admin")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> getSystemSummary()
        {
            var auctionCount = await _context.Auctions.CountAsync();
            var nftCount = await _context.Nfts.CountAsync();
            var userCount = await _userManager.Users.CountAsync();

            var summary = new
            {
                TotalAuctions = auctionCount,
                TotalNfts = nftCount,
                TotalUsers = userCount
            };

            return Ok(summary);
        }
    }
}
