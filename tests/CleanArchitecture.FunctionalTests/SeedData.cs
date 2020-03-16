using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using System.Linq;

namespace CleanArchitecture.FunctionalTests
{
    class SeedData
    {
        public static readonly Category category1 = new Category
        {
            Id = 1,
            Name = "Category 1"
        };

        public static readonly Category category2 = new Category
        {
            Id = 2,
            Name = "Category 2"
        };

        public static readonly Category category3 = new Category
        {
            Id = 3,
            Name = "Category 3"
        };

        public static readonly Product product1 = new Product
        {
            Id = 1,
            Name = "Product 1",
            Description = "Product 1 description",
            CategoryId = 1
        };

        public static readonly Product product2 = new Product
        {
            Id = 2,
            Name = "Product 2",
            Description = "Product 2 description",
            CategoryId = 2
        };

        public static readonly Product product3 = new Product
        {
            Id = 3,
            Name = "Product 3",
            Description = "Product 3 description",
            CategoryId = 3
        };

        public static void PopulateTestData(AppDbContext dbContext)
        {
            //Remove any Category or Product items if they exist.
            if (dbContext.Category.Any() || dbContext.Product.Any())
            {
                foreach (var item in dbContext.Category)
                {
                    dbContext.Remove(item);
                }

                foreach (var item in dbContext.Product)
                {
                    dbContext.Remove(item);
                }

                dbContext.SaveChanges();
            }

            dbContext.Category.Add(category1);
            dbContext.Category.Add(category2);
            dbContext.Category.Add(category3);

            dbContext.Product.Add(product1);
            dbContext.Product.Add(product2);
            dbContext.Product.Add(product3);

            dbContext.SaveChanges();
        }
    }
}
