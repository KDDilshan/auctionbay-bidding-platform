using Api.Dtos;
using Api.Entities;

namespace Api.Services.BidService
{
    public interface IBidService
    {
        public Task<string> PlaceBid(int auctionId, PlaceBidDto placeBidDto);

        public Task<Bid> GetHighest(int auctionId);
    }
}
