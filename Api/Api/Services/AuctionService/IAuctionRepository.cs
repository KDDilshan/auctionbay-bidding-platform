using Api.Entities;

namespace Api.Services.AuctionService
{
    public interface IAuctionRepository
    {
        Task <List<Auction>>GetAuctionWithDetailsAsync();
    }
}
