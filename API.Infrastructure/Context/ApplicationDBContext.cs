using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Data
{
    //Add-Migration deletedShippingMethod -context API.Infrastructure.Data.ApplicationDBContext
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<OrderItem>()
                .Property(o => o.PriceAtOrder)
                .HasPrecision(18, 2); // Adjust precision and scale as per your requirements

            modelBuilder.Entity<OrderItem>()
                .Property(o => o.UnitPrice)
                .HasPrecision(18, 2);
            */
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
                      .IsRequired(); // Ensure PaymentMethod is required
            });
            modelBuilder.Entity<OrderItem>()
            .ToTable("OrderItems") // Ensure the correct table name
            .Property(o => o.Price)
            .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        // public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }

}
