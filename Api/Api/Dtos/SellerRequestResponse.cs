namespace Api.Dtos
{
    public class SellerRequestResponse
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? Created { get; set; }
        public string? Address { get; set; }
        public DateTime? Dob { get; set; }
        public string? Status { get; set; }
    }
}
