using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bloomify.Models;

namespace Bloomify.Data
{
    // Make sure this inherits correctly from the base IdentityDbContext class
    public class AppDbContext : IdentityDbContext<BloomifyUser, IdentityRole<int>, int,
                                                 IdentityUserClaim<int>, IdentityUserRole<int>,
                                                 IdentityUserLogin<int>, IdentityRoleClaim<int>,
                                                 IdentityUserToken<int>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ShippingDetail> ShippingDetails { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }

        // Reference to old User table for migration purposes
        // Remove this after migration is complete
        public DbSet<BloomifyUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This needs to be called first to set up the ASP.NET Identity tables
            base.OnModelCreating(modelBuilder);

            // Configure relationships for Identity-related entities
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.userID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Users)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Products)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShoppingCart>()
                .HasOne(sc => sc.Users)
                .WithMany()
                .HasForeignKey(sc => sc.userID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, CategoryName = "Flowers" },
                new Category { CategoryID = 2, CategoryName = "Sweets" }
            );

            modelBuilder.Entity<Provider>().HasData(
                new Provider { ProviderID = 1, ProviderName = "Euroflora" },
                new Provider { ProviderID = 2, ProviderName = "Floom" },
                new Provider { ProviderID = 3, ProviderName = "Transflora" },
                new Provider { ProviderID = 4, ProviderName = "Global Rose" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductID = 1, ProductName = "Trandafir Roșu", Price = 15, CategoryID = 1, ImagePath = "path1", ProductDescription = "description 1", ProviderID = 1 },
                new Product { ProductID = 2, ProductName = "Lalea Galbenă", Price = 10, CategoryID = 2, ImagePath = "path2", ProductDescription = "description 2", ProviderID = 2 }
            );
        }
    }
}