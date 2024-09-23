namespace Api.Dtos
{
    public class NftBidsDto
    {
        public int NftId { get; set; }
        public string NftTitle { get; set; }
        public string NftDescription { get; set; }
        public List<BidDto> Bids { get; set; }
    }


    public class BidDto
    {
        public int BidId { get; set; }
        public long BidPrice { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
    
}
