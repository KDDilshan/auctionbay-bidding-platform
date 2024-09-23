using Api.Services.AuctionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        public readonly AuctionService _auctionService;

        public AuctionsController(AuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAuctionDetils()
        {
            var auctions=await _auctionService.GetAuctionDetailsAsync();
            return Ok(auctions);

        }
    }
}
