using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? Message { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsRead { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
