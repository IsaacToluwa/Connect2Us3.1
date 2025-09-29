using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Connect2Us3._01.Models;

namespace Connect2Us3._01.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision for SQL Server
            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Book>()
                .Property(b => b.RentalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Rental>()
                .Property(r => r.RentalFee)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);
        }

        public System.Data.Entity.DbSet<Connect2Us3._01.Models.Book> Books { get; set; }
        public System.Data.Entity.DbSet<Connect2Us3._01.Models.Category> Categories { get; set; }
        public System.Data.Entity.DbSet<Connect2Us3._01.Models.Author> Authors { get; set; }
        public System.Data.Entity.DbSet<Connect2Us3._01.Models.Order> Orders { get; set; }
        public System.Data.Entity.DbSet<Connect2Us3._01.Models.OrderDetail> OrderDetails { get; set; }
        public System.Data.Entity.DbSet<Connect2Us3._01.Models.Rental> Rentals { get; set; }
        public System.Data.Entity.DbSet<Connect2Us3._01.Models.Reservation> Reservations { get; set; }
        public System.Data.Entity.DbSet<Connect2Us3._01.Models.Delivery> Deliveries { get; set; }
        public System.Data.Entity.DbSet<Connect2Us3._01.Models.Payment> Payments { get; set; }
        public System.Data.Entity.DbSet<Connect2Us3._01.Models.Review> Reviews { get; set; }
        public System.Data.Entity.DbSet<Connect2Us3._01.Models.Wishlist> Wishlists { get; set; }
        public System.Data.Entity.DbSet<Connect2Us3._01.Models.Notification> Notifications { get; set; }
        public System.Data.Entity.DbSet<Connect2Us3._01.Models.ShoppingCart> ShoppingCarts { get; set; }
        public System.Data.Entity.DbSet<Connect2Us3._01.Models.CartItem> CartItems { get; set; }
    }
}