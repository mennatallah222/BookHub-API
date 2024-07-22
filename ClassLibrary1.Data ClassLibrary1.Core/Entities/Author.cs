using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Author
    {
        public Author()
        {
            WrittenBooks = new HashSet<Product>();
        }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product>? WrittenBooks { get; set; }


        // Navigation property for many-to-many relationship with User
        public virtual ICollection<User> FavouriteByUsers { get; set; }

    }
}
