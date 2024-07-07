using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Shipping
    {
        [Key]
        public int ShippingId { get; set; }

        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, Range(0.01, 1000)]
        public decimal BaseFee { get; set; }

        [Required]
        public DateTime ShippingDate { get; set; }
        /*  [Required, Range(1, 100)]
          public int EstimatedDeliveryTime { get; set; }//in days
          */
        public string? TrackingNumber { get; set; }
    }
}
