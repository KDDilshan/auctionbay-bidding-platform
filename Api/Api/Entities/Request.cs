using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entities
{
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string RequestDeails { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;

        public DateTime AcceptDate { get; set; }

        public string UserID { get; set; }

        public AppUser User { get; set; }   
    }
}
