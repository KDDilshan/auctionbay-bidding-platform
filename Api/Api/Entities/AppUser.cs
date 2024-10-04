using Microsoft.AspNetCore.Identity;

namespace Api.Entities
{
    public class AppUser:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public int ReqId { get; set; }

        public SellerRequest Requests { get; set; }

        public List<Nft> nfts { get; set; }

        public List<Auction> auctions { get; set; } 

        public List<Bid> Bids { get; set; }

        public List<PaymentRecord> PaymentRecords { get; set; }

      
    }
}
