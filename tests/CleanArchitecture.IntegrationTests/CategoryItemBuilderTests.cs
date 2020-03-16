using Xunit;

namespace CleanArchitecture.IntegrationTests
{
    public class CategoryItemBuilderTests
    {
        [Fact]
        public void CategoryItemBuilderConstructorIdTest()
        {
            var category = new CategoryItemBuilder().Id(1).Build();
            Assert.True(category.Id == 1);
        }

        [Fact]
        public void CategoryItemBuilderConstructorNameTest()
        {
            var category = new CategoryItemBuilder().Name("Test Category 1").Build();
            Assert.True(category.Name == "Test Category 1");
        }

        [Fact]
        public void CategoryItemBuilderWithDefaultValuesTest()
        {
            var category = new CategoryItemBuilder().WithDefaultValues().Build();

            Assert.NotNull(category);
            Assert.True(category.Id == 1);
            Assert.True(category.Name == "Test Category 1");
        }

        [Fact]
        public void CategoryItemBuilderTest()
        {
            var category = new CategoryItemBuilder().Build();
            category.Id = 2;
            category.Name = "Test Category 2";

            Assert.NotNull(category);
            Assert.True(category.Id == 2);
            Assert.True(category.Name == "Test Category 2");
        }
    }
}
