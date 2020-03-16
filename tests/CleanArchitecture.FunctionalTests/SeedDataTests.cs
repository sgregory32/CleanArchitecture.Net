using CleanArchitecture.Api;
using Xunit;

namespace CleanArchitecture.FunctionalTests
{
    public class SeedDataTests
    {
        [Fact]
        public void GetCategory1Test()
        {
            Assert.NotNull(SeedData.category1);
            Assert.True(SeedData.category1.Id == 1);
            Assert.True(SeedData.category1.Name == "Category 1");
        }

        [Fact]
        public void GetCategory2Test()
        {
            Assert.NotNull(SeedData.category2);
            Assert.True(SeedData.category2.Id == 2);
            Assert.True(SeedData.category2.Name == "Category 2");
        }

        [Fact]
        public void GetCategory3Test()
        {
            Assert.NotNull(SeedData.category3);
            Assert.True(SeedData.category3.Id == 3);
            Assert.True(SeedData.category3.Name == "Category 3");
        }

        [Fact]
        public void GetProduct1Test()
        {
            Assert.NotNull(SeedData.product1);
            Assert.True(SeedData.product1.Id == 1);
            Assert.True(SeedData.product1.Description == "Product 1 description");
            Assert.True(SeedData.product1.CategoryId == 1);
        }

        [Fact]
        public void GetProduct2Test()
        {
            Assert.NotNull(SeedData.product2);
            Assert.True(SeedData.product2.Id == 2);
            Assert.True(SeedData.product2.Description == "Product 2 description");
            Assert.True(SeedData.product2.CategoryId == 2);
        }

        [Fact]
        public void GetProduct3Test()
        {
            Assert.NotNull(SeedData.product3);
            Assert.True(SeedData.product3.Id == 3);
            Assert.True(SeedData.product3.Description == "Product 3 description");
            Assert.True(SeedData.product3.CategoryId == 3);
        }
    }
}
