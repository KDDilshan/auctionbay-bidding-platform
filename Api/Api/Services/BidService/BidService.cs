using Api.Data;
using Api.Dtos;
using Api.Entities;
using Api.Models.Email;
using Api.Services.EmailService;
using Api.Services.UserService;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.BidService
{
    public class BidService:IBidService
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly IEmailService _emailServide;

        public BidService(AppDbContext context, IUserService userService,IEmailService emailService)
        {
            _context = context;
            _userService = userService;
            _emailServide = emailService;
        }

        public async Task<Bid> GetHighest(int auctionId)
        {
            var highestBid = await _context.Bids.Where(b => b.AuctionID == auctionId).OrderByDescending(b => b.BidPrice).FirstOrDefaultAsync();
            return highestBid;
        }

        public async Task<string> PlaceBid(int auctionId, PlaceBidDto placeBidDto)
        {
            var auction = await _context.Auctions.Include(a=>a.AppUser).FirstOrDefaultAsync(a=>a.Id==auctionId);
            if (auction == null) return "not found";

            if (auction.Status != "Open") return "bad";

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
            _emailServide.Send(new NewBidNotificationEmail(auction.Title, value,user.FirstName,auction.AppUser.FirstName,auction.AppUser.Email));
            return "Bid placed successfully";
        }

    }
}
