namespace Api.Dtos
{
    public class CheakoutDto
    {
        public string NftTitle { get; set; }
        public string NftDescription { get; set; }

        public string BuyerId { get; set; }

        public int AuctionId { get; set; }
        public string BuyerUsername { get; set; }
        public long FinalPrice { get; set; }
    }
}
