using System.ComponentModel.DataAnnotations;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities
{
    public class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
            CurrentlyReading = new HashSet<Product>();
            WantToRead = new HashSet<Product>();
            ReadBooks = new HashSet<Product>();
            FavouriteAuthors = new HashSet<Author>();
            ReviewedBooks = new HashSet<Review>();
            Friends = new HashSet<Friendship>();
        }
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public Cart Cart { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product>? CurrentlyReading { get; set; }
        public virtual ICollection<Product>? WantToRead { get; set; }
        public virtual ICollection<Product>? ReadBooks { get; set; }

        public virtual ICollection<Author>? FavouriteAuthors { get; set; }

        public virtual ICollection<Review>? ReviewedBooks { get; set; }
        public virtual ICollection<Friendship>? Friends { get; set; }

    }
}
