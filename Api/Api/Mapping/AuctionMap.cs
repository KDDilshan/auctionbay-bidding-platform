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
                Price = auction.Price*100,
                NftId = auction.NftId,
                UserID = UserId,
                Category = auction.Category
            };
        }

        public static ClaimResponse ToClaim(this Auction auction,long highestBid)
        {
            return new ClaimResponse
            {
                Id = auction.Id,
                Name = auction.Nft.Title,
                Img = auction.Nft.Image,
                Auction = auction.Title,
                Amount = highestBid
            };
        }
    }
}
