namespace Api.Dtos
{
    public class AuctionResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public long Price { get; set; }
        public string NftTitle { get; set; }
        public int NumberOfBids {  get; set; }
        public long CurrentBid {  get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public string Owner { get; set; }
        public string email { get; set; }
        public string Status { get; set; }
    }
}
