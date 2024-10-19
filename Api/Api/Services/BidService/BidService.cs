using Api.Data;
using Api.Dtos;
using Api.Entities;
using Api.Services.UserService;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.BidService
{
    public class BidService:IBidService
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public BidService(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<Bid> GetHighest(int auctionId)
        {
            var highestBid = await _context.Bids.Where(b => b.AuctionID == auctionId).OrderByDescending(b => b.BidPrice).FirstOrDefaultAsync();
            return highestBid;
        }

        public async Task<string> PlaceBid(int auctionId, PlaceBidDto placeBidDto)
        {
            var auction = await _context.Auctions.FindAsync(auctionId);
            if (auction == null) return "not found";

            var highestBid = await _context.Bids.Where(b => b.AuctionID == auctionId).OrderByDescending(b => b.BidPrice).FirstOrDefaultAsync();

            long value = (long)placeBidDto.Price*100;

            var current = highestBid?.BidPrice ?? auction.Price;

            if (value <= current) return "low";

            var user = await _userService.getCurrentUser();

            _context.Bids.Add(new Bid
            {
                AuctionID = auctionId,
                BidPrice = value,
                UserId = user.Id
            });

            await _context.SaveChangesAsync();
            return "Bid placed successfully";
        }

    }
}
