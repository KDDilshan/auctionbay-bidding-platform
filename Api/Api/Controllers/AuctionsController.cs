using Api.Data;
using Api.Dtos;
using Api.Mapping;
using Api.Services.AuctionService;
using Api.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        public readonly AuctionService _auctionService;
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public AuctionsController(AuctionService auctionService, AppDbContext context, IUserService userService)
        {
            _auctionService = auctionService;
            _context = context;
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> GetAllAuctionDetils()
        {
            var auctions=await _auctionService.GetAuctionDetailsAsync();
            return Ok(auctions);

        }

        [HttpPost]
        [Authorize(Roles = "Seller")]
        public async Task<ActionResult> CreateAuction([FromBody] AuctionDto auctionDto)
        {
            try
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

                var nft = await _context.Nfts.FindAsync(auctionDto.NftId);
                if (nft == null) return NotFound("Nft not found");

                if (nft.UserId != user.Id) return Unauthorized("You are not the owner of this NFT");

                _context.Auctions.Add(auctionDto.ToEntity(user.Id));
                await _context.SaveChangesAsync();

                return Ok("ok");
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
