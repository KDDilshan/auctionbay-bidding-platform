using Api.Dtos;
using Api.Entities;

namespace Api.Mapping
{
    public static class AuctionMap
    {
        public static Auction ToEntity(this AuctionDto auction,string UserId)
        {
            return new Auction
            {
                Title = auction.Title,
                Description = auction.Description,
                StartDate = auction.StartDate,
                EndDate = auction.EndDate,
                Price = auction.Price,
                NftId = auction.NftId,
                UserID = UserId
            };
        }
    }
}
