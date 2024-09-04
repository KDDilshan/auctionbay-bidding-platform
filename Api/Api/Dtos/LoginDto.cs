using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public record class LoginDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
