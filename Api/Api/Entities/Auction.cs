using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Api.Entities
{
    public class Auction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public long Price { get; set; }
        public string Status { get; set; } = "Open";

        public int NftId {  get; set; }

        public Nft Nft { get; set; }

        public List<Bid> Bids { get; set; } 

        public List<PaymentRecord> PaymentRecords { get; set; }

        public string UserID { get; set; }

        public AppUser AppUser { get; set; }

    }
}
