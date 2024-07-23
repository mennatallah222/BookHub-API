using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Friendship
    {
        [Key, Column(Order = 0)]
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Key, Column(Order = 1)]
        public int? FriendId { get; set; }

        [ForeignKey("FriendId")]
        public User? Friend { get; set; }

        [Required]
        [MaxLength(20)]
        public string? FriendshipStatus { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
