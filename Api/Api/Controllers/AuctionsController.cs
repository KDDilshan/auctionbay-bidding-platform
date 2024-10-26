using Api.Data;
using Api.Dtos;
using Api.Entities;
using Api.Mapping;
using Api.Models.Email;
using Api.Services.AuctionService;
using Api.Services.BidService;
using Api.Services.EmailService;
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
        public readonly IAuctionService _auctionService;
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly IBidService _bidService;
        private readonly IEmailService _emailService;

        public AuctionsController(IAuctionService auctionService, AppDbContext context, IUserService userService, IBidService bidService, IEmailService emailService)
        {
            _auctionService = auctionService;
            _context = context;
            _userService = userService;
            _bidService = bidService;
            _emailService = emailService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionResponse>> GetAuction(int id)
        {
            var auction = await _context.Auctions.FindAsync(id);
            var nft = await _context.Nfts.FindAsync(auction.NftId);
            var user = await _context.Users.FindAsync(auction.UserID);
            var highestBid = await _bidService.GetHighest(auction.Id);
            var bidsCount = await _context.Bids.Where(b => b.AuctionID == id).CountAsync();
            //var userHighest = await _context.Bids.Where(b => b.AuctionID == id && b.UserId == curruser.Id).OrderByDescending(b => b.BidPrice).FirstOrDefaultAsync();
            return Ok(new AuctionResponse
            {
                Id = auction.Id,
                Title = auction.Title,
                Description = auction.Description,
                EndDate = auction.EndDate,
                Price = auction.Price,
                NftTitle = nft.Title,
                Image = nft.Image,
                Category = auction.Category,
                CurrentBid = highestBid?.BidPrice ?? auction.Price,
                NumberOfBids = bidsCount,
                Owner = user.FirstName + " " + user.LastName,
                email = user.Email,
                Status = auction.Status,
            });
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<List<AuctionResponse>>> GetAuctionsByCategory(string category)
        {
            try
            {
                List<Auction> auctions;
                if(category == "new") auctions = await _context.Auctions.Include(a => a.Nft).Where(a => a.Status=="Open").OrderByDescending(a => a.Id).ToListAsync();
                else auctions = await _context.Auctions.Include(a => a.Nft).Where(a => a.Category == category).OrderByDescending(a=>a.Id).ToListAsync();
                var response = new List<AuctionResponse>();

                foreach (var auction in auctions)
                {
                    var highestBid = await _bidService.GetHighest(auction.Id);
                    response.Add(new AuctionResponse
                    {
                        Id = auction.Id,
                        Title = auction.Title,
                        EndDate = auction.EndDate,
                        Image = auction.Nft.Image,
                        CurrentBid = highestBid?.BidPrice ?? auction.Price,
                        Status= auction.Status,
                    });
                }

                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("search")]
        public async Task<ActionResult<List<AuctionResponse>>> SearchAuctions(AuctionSearchDto searchDto)
        {
            try
            {
                List<Auction> auctions;

                if (string.IsNullOrWhiteSpace(searchDto.searchString))
                {
                    auctions = await _context.Auctions
                        .Include(a => a.Nft)
                        .OrderByDescending(a => a.Id)
                        .ToListAsync();
                }
                else
                {
                    var searchTerms = searchDto.searchString.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var searchPattern = string.Join("%", searchTerms);

                    auctions = await _context.Auctions
                        .Include(a => a.Nft)
                        .Where(a => EF.Functions.Like(a.Title.ToLower(), $"%{searchPattern.ToLower()}%"))
                        .OrderByDescending(a => a.Id)
                        .ToListAsync();
                }

                var response = new List<AuctionResponse>();

                foreach (var auction in auctions)
                {
                    var highestBid = await _bidService.GetHighest(auction.Id);
                    response.Add(new AuctionResponse
                    {
                        Id = auction.Id,
                        Title = auction.Title,
                        EndDate = auction.EndDate,
                        Image = auction.Nft.Image,
                        CurrentBid = highestBid?.BidPrice ?? auction.Price,
                        Status = auction.Status,
                    });
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("{id}/user")]
        [Authorize(Roles = "Seller")]
        public async Task<ActionResult> GetMyAuction(int id)
        {
            var userId = _userService.GetCurrentUserId();
            var auction = await _context.Auctions.Include(a=>a.Nft).FirstOrDefaultAsync(a=>a.Id==id);
            if(auction == null) return NotFound("Auction not found");
            if(auction.UserID != userId) return Unauthorized("You are not the owner of this auction");
            var highestBid = await _bidService.GetHighest(auction.Id);
            var bidsCount = await _context.Bids.Where(b => b.AuctionID == id).CountAsync();
            return Ok(new 
            {
                title=auction.Title,
                description=auction.Description,
                price=auction.Price/100,
                nftId=auction.Nft.Id,
                nftName=auction.Nft.Title,
                nftDes = auction.Nft.Description,
                img= auction.Nft.Image,
                CurrentBid = highestBid?.BidPrice ?? auction.Price,
                NumberOfBids = bidsCount,
                startDate = auction.StartDate,
                endDate = auction.EndDate,
                status = auction.Status,
                category = auction.Category
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

        [HttpGet("{id}/bids")]
        [Authorize(Roles = "Seller")]
        public async Task<ActionResult> GetAuctionBids(int id)
        {
            string userId = _userService.GetCurrentUserId();

            // Check if the auction exists and is owned by the current user
            var auction = await _context.Auctions
                .Where(a => a.Id == id && a.UserID == userId)
                .FirstOrDefaultAsync();

            if (auction == null)
            {
                return Unauthorized("You are not authorized to view bids for this auction.");
            }

            // Fetch all bids for the auction, selecting only BidDate and BidPrice
            var bids = await _context.Bids
                .Where(b => b.AuctionID == id)
                .Select(b => new
                {
                    b.BidDate,
                    b.BidPrice
                })
                .ToListAsync();

            return Ok(bids);
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
                if (nft == null) return NotFound("NFT not found");

                if (nft.UserId != user.Id) return Unauthorized("You are not the owner of this NFT");

                var auctions = await _context.Auctions.Where(a => a.NftId == auctionDto.NftId&&(a.Status=="Open"|| a.Status=="Close")).ToListAsync();
                if (auctions.Count > 0) return BadRequest("This NFT is already in auction");

                Auction auction = auctionDto.ToEntity(user.Id);

                _context.Auctions.Add(auction);
                await _context.SaveChangesAsync();

                _emailService.Send(new AuctionCreatedEmail(auction, user));

                return Ok("Auction Created.");
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Seller")]
        public async Task<ActionResult> UpdateAuction(int id, [FromBody] AuctionDto auctionDto)
        {
            try
            {
                var auction = await _context.Auctions.FindAsync(id);
                if (auction == null) return NotFound("Auction not found");
                if (auction.Status == "Close") return BadRequest("Auction is closed.");
                if (auction.Status == "Sold") return BadRequest("Auction is Sold.");

                var userId = _userService.GetCurrentUserId();
                if (auction.UserID != userId) return Unauthorized("You are not the owner of this auction");

                auction.Title = auctionDto.Title;
                auction.Description = auctionDto.Description;
                auction.Price = auctionDto.Price*100;
                auction.EndDate = auctionDto.EndDate;
                auction.StartDate = auctionDto.StartDate;
                auction.Category = auctionDto.Category;
                _context.Auctions.Update(auction);
                await _context.SaveChangesAsync();

                return Ok("Auction Updated.");

            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("user")]
        [Authorize(Roles = "Seller")]
        public async Task<ActionResult<List<AuctionResponse>>> GetMyAuctions()
        {
            var userId = _userService.GetCurrentUserId();
            var auctions = await _context.Auctions.Where(a => a.UserID==userId).Include(a=>a.Nft).ToListAsync();
            var response = new List<AuctionResponse>();

            foreach (var auction in auctions)
            {
                var nft = await _context.Nfts.FindAsync(auction.NftId);
                var highestBid = await _bidService.GetHighest(auction.Id);
                response.Add(new AuctionResponse
                {
                    Id = auction.Id,
                    Title = auction.Title,
                    EndDate = auction.EndDate,
                    Price = auction.Price,
                    NftTitle = auction.Nft.Title,
                    CurrentBid = highestBid?.BidPrice ?? auction.Price,
                    Status = auction.Status,
                });
            }

            return Ok(response);
        }

        [HttpGet("claims")]
        [Authorize]
        public async Task<ActionResult<List<ClaimResponse>>> GetClaims()
        {
            var user = await _userService.getCurrentUser();
            var auctions = await _context.Auctions.Include(a=>a.Nft).Where(a => a.Winner == user.Id && a.Status == "Close").ToListAsync();
            var response = new List<ClaimResponse>();

            foreach (var auction in auctions)
            {
                
                var highestBid = await _bidService.GetHighest(auction.Id);
               
                response.Add(auction.ToClaim(highestBid.BidPrice));
            }

            return Ok(response);
        }
    }
}
