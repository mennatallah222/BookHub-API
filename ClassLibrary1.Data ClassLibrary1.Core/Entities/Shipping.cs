using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Required]
        public int ShippingMethodId { get; set; }
        [ForeignKey("ShippingMethodId")]
        public ShippingMethod ShippingMethod { get; set; }

        [Required]
        public DateTime ShippingDate { get; set; }

        public string? TrackingNumber { get; set; }
    }
}
