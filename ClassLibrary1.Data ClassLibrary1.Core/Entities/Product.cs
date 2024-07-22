using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Product
    {
        public Product()
        {
            Reviews = new HashSet<Review>();
            BookGenres = new HashSet<BookGenre>();
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
        public string? ISBN { get; set; }
        public string? Image { get; set; }
        public int? PagesNumber { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string? Language { get; set; }

        public int? CartId { get; set; }
        [ForeignKey("CartId")]
        public virtual Cart? Cart { get; set; }
        public int? AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author? Author { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<BookGenre>? BookGenres { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }


        // Navigation properties for many-to-many relationships
        public virtual ICollection<User> CurrentlyReadingUsers { get; set; }
        public virtual ICollection<User> WantToReadUsers { get; set; }
        public virtual ICollection<User> ReadUsers { get; set; }


    }
}
