using Api.Dtos;

namespace Api.Services.BidService
{
    public interface IBidService
    {
        public Task<string> PlaceBid(int auctionId, PlaceBidDto placeBidDto);
    }
}
