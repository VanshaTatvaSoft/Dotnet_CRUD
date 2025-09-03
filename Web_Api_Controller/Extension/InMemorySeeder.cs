using Web_Api_Repository.Models;

namespace Web_Api_Controller.Extension;

public static class InMemorySeeder
{
    public static void InMemmoryDbSeeder(ProductManagementContext context)
    {
        if(!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Web_Api_Repository.Models.Category
                { CategoryName = "Pizza", CategoryDesc = "Delicious pizzas", IsDeleted = false, CreatedAt = DateTime.UtcNow },
                new Web_Api_Repository.Models.Category
                { CategoryName = "Pasta", CategoryDesc = "Italian pastas", IsDeleted = false, CreatedAt = DateTime.UtcNow },
                new Web_Api_Repository.Models.Category
                { CategoryName = "Drinks", CategoryDesc = "Soft drinks and beverages", IsDeleted = false, CreatedAt = DateTime.UtcNow },
                new Web_Api_Repository.Models.Category
                { CategoryName = "Burgers", CategoryDesc = "Tasty burgers", IsDeleted = false, CreatedAt = DateTime.UtcNow },
                new Web_Api_Repository.Models.Category
                { CategoryName = "Salads", CategoryDesc = "Healthy salads", IsDeleted = false, CreatedAt = DateTime.UtcNow }
            );
            context.SaveChanges(); 
        }
    }
}
