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
using Api.Mapping;
using Api.Services.EmailService;
using Api.Models.Email;

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
        private readonly IEmailService _emailService;

        public PaymentController(StripePaymentService stripePaymentService,AppDbContext appDbContext, IUserService userService, IBidService bidService,IEmailService emailService)
        {
            _paymentService = stripePaymentService;
            _context = appDbContext;
            _userService = userService;
            _bidService = bidService;
            _emailService = emailService;
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

            var session = _paymentService.CreateCheckoutSession(auction, highestBid);

            _context.PaymentRecords.Add(new PaymentRecord
            {
                StripeSessionId = session.Id,
                UserId = auction.Winner,
                AuctionId = auction.Id,
                Created = DateTime.UtcNow,
                Status = "Pending"

            });
            _context.SaveChanges();

            return Ok(new { url= session.Url });
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> Success(string sessionId)
        {
            var paymentRecord = await _context.PaymentRecords.Include(a=>a.User).FirstOrDefaultAsync(p => p.StripeSessionId == sessionId);
            var status = "bad";
            if (paymentRecord != null)
            {
                var session = await _paymentService.CheckSession(sessionId);
                // Check if the payment was successful
                if (session.PaymentStatus == "paid")
                {
                    paymentRecord.Status = "Success";
                    var auction = await _context.Auctions.Include(a=>a.AppUser).FirstOrDefaultAsync(a=>a.Id==paymentRecord.AuctionId);
                    auction.Status = "Sold";

                    var nft = await _context.Nfts.FindAsync(auction.NftId);
                    nft.UserId = paymentRecord.UserId;

                    await _context.SaveChangesAsync();
                    status= "ok";
                    var highestBid = await _bidService.GetHighest(auction.Id);
                    _emailService.Send(new AuctionWinnerClaimedEmail(auction.Title, paymentRecord.User.FirstName, paymentRecord.User.Email, nft.Title, nft.Description));
                    _emailService.Send(new AuctionNftClaimedEmail(auction.AppUser.FirstName, auction.AppUser.Email, nft.Title, paymentRecord.User.FirstName, highestBid.BidPrice, DateTime.UtcNow));
                }
                else
                {
                    paymentRecord.Status = "Cancelled";
                    await _context.SaveChangesAsync();
                }
            }

            return Redirect("http://localhost:3000/account/inventory?status="+status);
        }

        [HttpGet("transactions")]
        [Authorize]
        public async Task<ActionResult<List<TransactionResponse>>> GetMyTransactions()
        {
            try
            {
                var useId = _userService.GetCurrentUserId();
                var transactions = await _context.PaymentRecords.Include(a => a.Auction.Nft).Where(a => a.UserId == useId).ToListAsync();
                var response = new List<TransactionResponse>();

                foreach (var payment in transactions)
                {
                    var highestBid = await _bidService.GetHighest(payment.Auction.Id);
                    response.Add(payment.ToDto(highestBid.BidPrice));
                }

                return Ok(response);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
