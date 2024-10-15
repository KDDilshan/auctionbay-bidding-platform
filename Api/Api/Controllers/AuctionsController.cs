using Api.Data;
using Api.Dtos;
using Api.Entities;
using Api.Mapping;
using Api.Services.AuctionService;
using Api.Services.BidService;
using Api.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        public readonly AuctionService _auctionService;
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly IBidService _bidService;

        public AuctionsController(AuctionService auctionService, AppDbContext context, IUserService userService, IBidService bidService)
        {
            _auctionService = auctionService;
            _context = context;
            _userService = userService;
            _bidService = bidService;
        }

        [HttpGet]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> GetAllAuctionDetils()
        {
            var auctions=await _auctionService.GetAuctionDetailsAsync();
            return Ok(auctions);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionResponse>> GetAuction(int id)
        {
            var auction = await _context.Auctions.FindAsync(id);
            var nft = await _context.Nfts.FindAsync(auction.NftId);
            var user = await _context.Users.FindAsync(auction.UserID);
            var highestBid = await _context.Bids.Where(b => b.AuctionID == id).OrderByDescending(b => b.BidPrice).FirstOrDefaultAsync();
            var bidsCount = await _context.Bids.Where(b => b.AuctionID == id).CountAsync();
            //var userHighest = await _context.Bids.Where(b => b.AuctionID == id && b.UserId == curruser.Id).OrderByDescending(b => b.BidPrice).FirstOrDefaultAsync();
            return Ok(new AuctionResponse
            {
                Title = auction.Title,
                Description = auction.Description,
                EndDate = auction.EndDate,
                Price = auction.Price,
                NftTitle = nft.Title,
                Image = nft.Image,
                Category = "Art",
                CurrentBid = highestBid?.BidPrice ?? auction.Price,
                NumberOfBids = bidsCount,
                Owner = user.FirstName + " " + user.LastName,
                email = user.Email,
            });
        }

        [HttpPost("{id}/bid")]
        [Authorize]
        public async Task<ActionResult> PlaceBid(int id,PlaceBidDto placeBidDto)
        {
            
            var result = await _bidService.PlaceBid(id, placeBidDto);

            if (result == "not found") return NotFound("Auction not found");
            if (result == "low") return BadRequest("Bid price must be higher than current bid price");

            return Ok(result);
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
