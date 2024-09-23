namespace Api.Dtos
{
    public class AuctionDetailsDto
    {
        public string NftTitle { get; set; }

        public string FirstBidAmount { get; set; }

        public int NumberOfBids {  get; set; }

        public long HighestBid {  get; set; }

        public string TimeRemaining { get; set; }
    }
}
