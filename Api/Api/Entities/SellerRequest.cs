using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    public class SellerRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;

        public DateTime AcceptDate { get; set; }

        public string UserId { get; set; }

        public AppUser User { get; set; }

        [Required]
        public string IdPhotoPath { get; set; } = string.Empty; 

        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
