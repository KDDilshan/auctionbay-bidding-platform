namespace Api.Entities
{
    public class Bid
    {
        public int Id { get; set; }

       public DateTime BidDate { get; set; }= DateTime.Now;

        public double BidPrice { get; set; }

        public int AuctionID {  get; set; }

        public Auction Auction { get; set; } 

        public string UserId { get; set; }

        public AppUser AppUsers { get; set; } 

    }
}
