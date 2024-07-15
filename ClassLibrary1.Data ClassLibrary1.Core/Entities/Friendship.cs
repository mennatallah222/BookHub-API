using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Friendship
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int FriendId { get; set; }
        [ForeignKey("FriendId")]
        public User Friend { get; set; }
    }
}
