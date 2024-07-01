using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Category
    {
        public Category()
        {
            Products= new HashSet<Product>();
        }
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
