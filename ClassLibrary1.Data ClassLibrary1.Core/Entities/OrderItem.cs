/*using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtOrder { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("OrderId")]
        [Required]
        public virtual Order Order { get; set; }
        [ForeignKey("ProductId")]
        [Required]
        public virtual Product Product { get; set; }
        
    }
}
*/