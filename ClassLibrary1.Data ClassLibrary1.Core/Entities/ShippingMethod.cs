using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class ShippingMethod
    {
        [Key]
        public int ShippingMethodId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, Range(1, 100)]
        public int EstimatedDeliveryTime { get; set; } // In days
    }

}
