using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Cart
    {
        public Cart()
        {
            Products = new HashSet<Product>();
        }


        [Key]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
