using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }
        [Key]
        public int Id { get; set; }
        // public decimal TotalAmount { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
