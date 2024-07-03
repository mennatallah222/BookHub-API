using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Order
    {
        public Order()
        {
            Products = new HashSet<Product>();
        }
        [Key]
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string ShippingAddress { get; set; } = "";
        public string? PaymentStatus { get; set; } = "Pending";
        public string PaymentMethod { get; set; } = "cash";
        public string TrackingNumber { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("CustomerId")]
        //[InverseProperty("OrderId")]
        public virtual Customer Customer { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
