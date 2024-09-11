using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    public class Nft
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string description { get; set; }

        public string UserId { get; set; }

        public AppUser User { get; set; }

        public List<Auction> Auctions { get; set; }
    }
}
