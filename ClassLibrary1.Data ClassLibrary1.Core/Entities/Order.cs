using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }
        [Key]
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string ShippingAddress { get; set; } = "";
        public string? PaymentStatus { get; set; } = "Pending";
        public string PaymentMethod { get; set; } = "cash";
        public string TrackingNumber { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}
