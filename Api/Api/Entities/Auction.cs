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

        public string title { get; set; }

        public string description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Double Price { get; set; }

        public int NftId {  get; set; }

        public Nft Nft { get; set; }

        public List<Bid> Bids { get; set; } 

        public string UserID { get; set; }

        public AppUser AppUser { get; set; }

    }
}
