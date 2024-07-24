using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();

            Orders = new HashSet<Order>();
            CurrentlyReading = new HashSet<Product>();
            WantToRead = new HashSet<Product>();
            ReadBooks = new HashSet<Product>();
            FavouriteAuthors = new HashSet<Author>();
            ReviewedBooks = new HashSet<Review>();
            FirendInitiated = new HashSet<Friendship>();
            FirendRecieved = new HashSet<Friendship>();
            NotificationsBox = new HashSet<Notification>();
        }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        [EncryptColumn]
        public string? Code { get; set; }
        public string? PhoneNumber { get; set; }
        [InverseProperty(nameof(UserRefreshToken.User))]
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }



        public bool IsDeleted { get; set; }
        public Cart Cart { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product>? CurrentlyReading { get; set; }
        public virtual ICollection<Product>? WantToRead { get; set; }
        public virtual ICollection<Product>? ReadBooks { get; set; }

        public virtual ICollection<Author>? FavouriteAuthors { get; set; }

        public virtual ICollection<Review>? ReviewedBooks { get; set; }
        public virtual ICollection<Friendship>? FirendInitiated { get; set; }
        public virtual ICollection<Friendship>? FirendRecieved { get; set; }
        public virtual ICollection<Notification>? NotificationsBox { get; set; }
    }
}
