using Api.Dtos;
using MimeKit.Encodings;

namespace Api.Services.AuctionService
{
    public class AuctionService
    {
        private readonly IAuctionRepository _auctionRepository;

        public AuctionService(IAuctionRepository auctionRepository)
        {         
            _auctionRepository = auctionRepository;
        }

        public async Task<List<AuctionDetailsDto>> GetAuctionDetailsAsync()
        {
            var auctions =await _auctionRepository.GetAuctionWithDetailsAsync();
            
            var auctionDetailsList= new List<AuctionDetailsDto>();

            foreach (var auction in auctions)
            {
                var firstBid = auction.Price;

                var bidCount = auction.Bids.Count;

                var hightstBid = auction.Bids.Any() ? auction.Bids.Max(b => b.BidPrice) : 0;

                TimeSpan timeRemaining = auction.EndDate - DateTime.Now;
                if (timeRemaining < TimeSpan.Zero)
                {
                    timeRemaining = TimeSpan.Zero;//auciton is ended
                }

                string formattedTimeRemaining;
                if (timeRemaining.Days > 0)
                {
                    formattedTimeRemaining = $"{timeRemaining.Days}D:{timeRemaining.Hours}H:{timeRemaining.Minutes}M";
                }
                else
                {
                    formattedTimeRemaining = $"{timeRemaining.Hours}H:{timeRemaining.Minutes}M:{timeRemaining.Seconds}S";
                }


                auctionDetailsList.Add(new AuctionDetailsDto
                {
                    NftTitle = auction.Nft.Title,
                    FirstBidAmount = firstBid.ToString(),
                    NumberOfBids = bidCount,
                    HighestBid = hightstBid,
                    TimeRemaining = formattedTimeRemaining
                });
            }

            return auctionDetailsList;
            
        }
    }
}
