using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Product
    {
        public Product()
        {
            Reviews = new HashSet<Review>();
        }
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public Product(string name)
        {
            Name = name;
        }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public int? OrderId { get; set; }
        [ForeignKey("OrderId")]
       //( [InverseProperty("ProductId")]
        public virtual Order? Order {  get; set; }
        
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
        public string? Image { get; set; }
        public int? CartId { get; set; }
        [ForeignKey("CartId")]
        public virtual Cart? Cart { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }


    }
}
