using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public record class SellerRequestDto
    {
        [Required]
        public IFormFile? Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
