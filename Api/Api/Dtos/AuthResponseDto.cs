namespace Api.Dtos
{
    public class AuthResponseDto
    {
        public string? Token { get; set; } = string.Empty;
        public string? Message { get; set; }

        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
        public string? Email { get; set; } 
    }
}
