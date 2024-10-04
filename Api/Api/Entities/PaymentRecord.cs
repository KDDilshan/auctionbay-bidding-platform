using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    public class PaymentRecord
    {
        [Key]
        public string StripeSessionId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Auction")]
        public int AuctionId { get; set; }

        public DateTime Created { get; set; }

        public string Status { get; set; }

        public virtual AppUser User { get; set; }
        public virtual Auction Auction { get; set; }
    }


}
