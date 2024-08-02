
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Data
{
    //Add-Migration deletedShippingMethod -context API.Infrastructure.Data.ApplicationDBContext
    public class ApplicationDBContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            // Ensure the encryption key is of correct length
            _encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");

        }




        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Notification> Notifications { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);


            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.TotalAmount)
                      .HasColumnType("decimal(18,2)");

                entity.Property(e => e.PaymentMethod)
                      .IsRequired();
            });
            modelBuilder.Entity<OrderItem>()
            .ToTable("OrderItems")
            .Property(o => o.Price)
            .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(p => p.User)
                    .WithMany()
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(u => u.CurrentlyReading)
                    .WithMany(p => p.CurrentlyReadingUsers)
                    .UsingEntity(j => j.ToTable("UserCurrentlyReading"));

                entity.HasMany(u => u.WantToRead)
                    .WithMany(p => p.WantToReadUsers)
                    .UsingEntity(j => j.ToTable("UserWantToRead"));

                entity.HasMany(u => u.ReadBooks)
                    .WithMany(p => p.ReadUsers)
                    .UsingEntity(j => j.ToTable("UserReadBooks"));

                entity.HasMany(u => u.FavouriteAuthors)
                    .WithMany(a => a.FavouriteByUsers)
                    .UsingEntity(j => j.ToTable("UserFavouriteAuthors"));

                entity.HasMany(u => u.ReviewedBooks)
               .WithOne(r => r.User)
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Friendship>()
            .HasKey(f => new { f.UserId, f.FriendId });

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.User)
                .WithMany(u => u.FirendInitiated)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.Friend)
                .WithMany(u => u.FirendRecieved)
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.UseEncryption(_encryptionProvider);


            base.OnModelCreating(modelBuilder);

        }
    }
}