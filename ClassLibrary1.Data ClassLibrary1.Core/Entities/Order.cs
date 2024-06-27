using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Order
    {
        public Order() {
            Products=new HashSet<Product>();
        }
        [Key]
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime OrderDate { get; set; }
        
        [ForeignKey("CustomerId")]
        //[InverseProperty("OrderId")]
        public virtual Customer Customer { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }

    }
}
