using Api.Data;
using Api.Entities;
using Api.Dtos;
using Api.Services.PaymentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Api.Services.UserService;
using Api.Services.BidService;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly StripePaymentService _paymentService;
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly IBidService _bidService;

        public PaymentController(StripePaymentService stripePaymentService,AppDbContext appDbContext, IUserService userService, IBidService bidService)
        {
            _paymentService = stripePaymentService;
            _context = appDbContext;
            _userService = userService;
            _bidService = bidService;
        }

        [Authorize]
        [HttpPost("{id}")]
        public async Task<ActionResult> CreateCheckoutSession(int id)
        {
            var userId = _userService.GetCurrentUserId();

            var auction = await _context.Auctions.Include(a=>a.Nft).Include(a=>a.WinUser).FirstOrDefaultAsync(a=>a.Id==id);

            if (auction == null)
            {
                return NotFound();
            }

            if(auction.Status != "Close")
            {
                return BadRequest("Auction is not closed yet");
            }

            if(auction.Winner != userId)
            {
                return BadRequest("You are not the winner of this auction");
            }

            var highestBid = await _bidService.GetHighest(auction.Id);

            var session = _paymentService.CreateCheckoutSession(
                auction,
                highestBid
            );

            _context.PaymentRecords.Add(new PaymentRecord
            {
                StripeSessionId = session.Id,
                UserId=userId,
                AuctionId=auction.Id,
                Created = DateTime.UtcNow,
                Status = "Pending"

            });
            _context.SaveChanges();

            return Ok(new { url= session.Url });
        }

        [HttpGet("success")]
        public async Task<IActionResult> Success(string sessionId)
        {
            var paymentRecord = await _context.PaymentRecords.FirstOrDefaultAsync(p => p.StripeSessionId == sessionId);
            if (paymentRecord != null)
            {
                paymentRecord.Status = "Success";
                var auction = await _context.Auctions.FindAsync(paymentRecord.AuctionId);
                auction.Status = "Sold";

                var nft = await _context.Nfts.FindAsync(auction.NftId);
                nft.UserId = paymentRecord.UserId;

                await _context.SaveChangesAsync();
            }

            return Redirect("http://localhost:3000/account/inventory?status=ok");
        }

        [HttpGet("cancel")]
        public async Task<IActionResult> Cancel(string sessionId)
        {
            var paymentRecord = await _context.PaymentRecords.FirstOrDefaultAsync(p => p.StripeSessionId == sessionId);
            if (paymentRecord != null)
            {
                paymentRecord.Status = "Cancelled";
                await _context.SaveChangesAsync();
            }

            return Redirect("http://localhost:3000/account/inventory?status=bad");
        }
    }
}
