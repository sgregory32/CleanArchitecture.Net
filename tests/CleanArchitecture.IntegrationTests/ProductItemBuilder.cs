using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.IntegrationTests
{
    public class ProductItemBuilder
    {
        private Product _product = new Product();
        private Category _category = new Category();

        public ProductItemBuilder Id(int id)
        {
            _product.Id = id;
            return this;
        }

        public ProductItemBuilder Name(string name)
        {
            _product.Name = name;
            return this;
        }

        public ProductItemBuilder Description(string description)
        {
            _product.Description = description;
            return this;
        }

        public ProductItemBuilder CategoryId(int categoryId)
        {
            _product.CategoryId = categoryId;
            return this;
        }

        public ProductItemBuilder Category(Category category)
        {
            _product.Category = category;
            return this;
        }

        public ProductItemBuilder WithDefaultValues()
        {
            _category = new Category() { Id = 3, Name = "Test Category 1" };
            _product = new Product() { Id = 1, Name = "Test Product 1", Description = "Test Product 1 Description", CategoryId = 3, Category = _category};
            return this;
        }

        public Product Build() => _product;
    }
}
