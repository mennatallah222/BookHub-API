using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public int? OrderId { get; set; }
        [ForeignKey("OrderId")]
       //( [InverseProperty("ProductId")]
        public virtual Order Order {  get; set; }
    }
}
