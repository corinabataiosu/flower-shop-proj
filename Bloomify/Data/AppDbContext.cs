using Microsoft.EntityFrameworkCore;
using Bloomify.Models;

namespace Bloomify.Data
{
    public class AppDbContext : DbContext
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
        public DbSet<User> Users { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, Email = "test1@example.com", Password = "test", Name = "Test User 1", Address = "address", PhoneNumber = "0711111111"},
                new User { UserID = 2, Email = "test2@example.com", Password = "test", Name = "Test User 2", Address = "address", PhoneNumber = "0711111111" }
            );
        }

    }
}
