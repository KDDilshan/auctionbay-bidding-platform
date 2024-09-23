using Api.Data;
using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.AuctionService
{
    public class AuctionRepository:IAuctionRepository
    {
        private readonly AppDbContext _context;

        public AuctionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Auction>> GetAuctionWithDetailsAsync()
        {
            return await _context.Auctions
                        .Include(a => a.Nft)
                        .Include(a => a.Bids)
                        .Include(a => a.AppUser)
                        .ToListAsync();
        }
    } 
}
