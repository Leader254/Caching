using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RedisCaching.Models;

namespace RedisCaching.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var products = GenerateDummyProducts(100);

            modelBuilder.Entity<Product>().HasData(products);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        private static Product[] GenerateDummyProducts(int count)
        {
            var random = new Random();
            var products = new Product[count];
            for (int i = 1; i <= count; i++)
            {

                var name = $"Product {i}";

                var category = random.Next(0, 3) switch
                {
                    0 => "Fruits",
                    1 => "Vegetables",
                    _ => "Beverages"
                };
                var price = random.Next(10, 1001);
                var quantity = random.Next(10, 201);
                products[i - 1] = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Category = category,
                    Price = price,
                    Quantity = quantity
                };
            }
            return products;
        }
    }
}
