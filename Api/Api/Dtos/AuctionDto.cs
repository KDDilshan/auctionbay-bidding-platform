namespace Api.Dtos
{
    public record class AuctionDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long Price { get; set; }
        public int NftId { get; set; }
        public string Category { get; set; }
    }
}
