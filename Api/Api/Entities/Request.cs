namespace Api.Entities
{
    public class Request
    {
        public int Id { get; set; }

        public string RequestDeails { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;

        public DateTime AcceptDate { get; set; }

        public string UserID { get; set; }

        public AppUser User { get; set; }   
    }
}
