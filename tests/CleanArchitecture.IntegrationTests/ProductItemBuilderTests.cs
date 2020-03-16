using CleanArchitecture.Core.Entities;
using Xunit;

namespace CleanArchitecture.IntegrationTests
{
    public class ProductItemBuilderTests
    {
        [Fact]
        public void ProductItemBuilderConstructorIdTest()
        {
            var product = new ProductItemBuilder().Id(1).Build();
            Assert.True(product.Id == 1);
        }

        [Fact]
        public void ProductItemBuilderConstructorNameTest()
        {
            var product = new ProductItemBuilder().Name("Test Product 1").Build();
            Assert.True(product.Name == "Test Product 1");
        }

        [Fact]
        public void ProductItemBuilderConstructorDescriptionTest()
        {
            var product = new ProductItemBuilder().Description("Test Product 1 Description").Build();
            Assert.True(product.Description == "Test Product 1 Description");
        }

        [Fact]
        public void ProductItemBuilderConstructorCategoryTest()
        {
            var category = new CategoryItemBuilder().WithDefaultValues().Build();
            var product = new ProductItemBuilder().Category(category).Build();
            Assert.NotNull(product.Category);
            Assert.True(product.Category.Id == 1);
            Assert.True(product.Category.Name == "Test Category 1");
        }

        [Fact]
        public void ProductItemBuilderConstructorCategoryIdTest()
        {
            var product = new ProductItemBuilder().CategoryId(1).Build();
            Assert.True(product.CategoryId == 1);
        }
    }
}
