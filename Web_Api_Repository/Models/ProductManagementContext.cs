using Microsoft.EntityFrameworkCore;

namespace Web_Api_Repository.Models;

public class ProductManagementContext: DbContext
{
    public ProductManagementContext() { }
    public ProductManagementContext(DbContextOptions<ProductManagementContext> options): base(options) {}

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity => 
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<Category>(entity => 
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, CategoryName = "Pizza", CategoryDesc = "Delicious pizzas", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = 2, CategoryName = "Pasta", CategoryDesc = "Italian pastas", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = 3, CategoryName = "Drinks", CategoryDesc = "Soft drinks and beverages", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = 4, CategoryName = "Burgers", CategoryDesc = "Tasty burgers", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = 5, CategoryName = "Salads", CategoryDesc = "Healthy salads", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = 6, CategoryName = "Desserts", CategoryDesc = "Sweet treats", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = 7, CategoryName = "Seafood", CategoryDesc = "Fresh seafood dishes", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = 8, CategoryName = "Soups", CategoryDesc = "Hot and tasty soups", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = 9, CategoryName = "Sandwiches", CategoryDesc = "Delicious sandwiches", IsDeleted = false, CreatedAt = DateTime.UtcNow },
            new Category { Id = 10, CategoryName = "Breakfast", CategoryDesc = "Morning meals", IsDeleted = false, CreatedAt = DateTime.UtcNow }
        );
    }
}
