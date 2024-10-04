using Api.Data;
using Api.Entities;
using Api.Dtos;
using Api.Services.PaymentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly StripePaymentService _paymentService;
        private readonly AppDbContext _context;

        public PaymentController(StripePaymentService stripePaymentService,AppDbContext appDbContext)
        {
            _paymentService = stripePaymentService;
            _context = appDbContext;
        }

        [HttpPost("NFT-checkout")]
        public ActionResult CreateCheckoutSession([FromBody] CheakoutDto cheakoutDto)
        {
            if (cheakoutDto == null)
            {
                return BadRequest("Invalid checkout details provided.");
            }

            var session = _paymentService.CreateCheckoutSession(
               "https://yourdomain.com/payment/success?sessionId={CHECKOUT_SESSION_ID}",
               "https://yourdomain.com/payment/cancel?sessionId={CHECKOUT_SESSION_ID}",
               cheakoutDto
            );

            // database ekata details add krenwa
            _context.PaymentRecords.Add(new PaymentRecord
            {
                StripeSessionId = session.Id,
                UserId=cheakoutDto.BuyerId.ToString(),
                AuctionId=cheakoutDto.AuctionId,
                Created = DateTime.UtcNow,
                Status = "Created"

            });
            _context.SaveChanges();

            return Ok(new { sessionId = session.Id });
        }

        [HttpGet("success")]
        public async Task<IActionResult> Success(string sessionId)
        {
            //cheak krnewa return krpi session id ekai databse eke id ekai harida kiyala
            var paymentRecord = await _context.PaymentRecords.FirstOrDefaultAsync(p => p.StripeSessionId == sessionId);
            if (paymentRecord != null)
            {
                paymentRecord.Status = "Success";
                await _context.SaveChangesAsync();
            }

            // Redirect to a success page or return success response
            return Ok("Payment successful.");
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

            // Redirect to a cancel page or return cancel response
            return Ok("Payment cancelled.");
        }
    }
}
