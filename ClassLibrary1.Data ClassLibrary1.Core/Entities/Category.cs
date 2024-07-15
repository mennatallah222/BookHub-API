using System.ComponentModel.DataAnnotations;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Category
    {
        public Category()
        {
            //  Products = new HashSet<Product>();
            BookGenres = new HashSet<BookGenre>();
        }
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        //public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<BookGenre>? BookGenres { get; set; }
    }
}
