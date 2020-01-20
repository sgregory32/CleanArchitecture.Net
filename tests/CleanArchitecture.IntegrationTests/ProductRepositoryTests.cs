using CleanArchitecture.Core.Entities;
using Xunit;

namespace CleanArchitecture.IntegrationTests
{
    public class ProductRepositoryTests : AsyncRepositoryTestFixture
    {
        [Fact]
        public async void GetByIdAsync()
        {
            var _repository = GetProductRepository();
            var item = new ProductItemBuilder().WithDefaultValues().Build();

            await _repository.AddAsync(item);

            var product = await _repository.GetByIdAsync(e => e.Id == item.Id, r => r.Category);

            Assert.Equal(item.Id, product.Id);
            Assert.Equal(item.Name, product.Name);
        }

        [Fact]
        public async void ListAsync()
        {
            var _repository = GetProductRepository();
            var item1 = new ProductItemBuilder().WithDefaultValues().Build();
            var product1 = await _repository.AddAsync(item1);

            var item2 = new ProductItemBuilder().Build();
            item2.Name = "Test Product 2";
            item2.Description = "Test Product 2 Description";
            item2.CategoryId = 2;
            item2.Category = new Category { Id = 2, Name = "Test Category 2" };
            var product2 = await _repository.AddAsync(item2);

            var result = await _repository.ListAsync();

            Assert.Equal(product1.Id, result[0].Id);
            Assert.Equal(product1.Name, result[0].Name);
            Assert.Equal(product1.Description, result[0].Description);
            Assert.Equal(product1.CategoryId, result[0].CategoryId);
            Assert.Equal(product1.Category.Id, result[0].Category.Id);
            Assert.Equal(product1.Category.Name, result[0].Category.Name);

            Assert.True(result[1].Id > 0);
            Assert.Equal(product2.Name, result[1].Name);
            Assert.Equal(product2.Description, result[1].Description);
            Assert.Equal(product2.CategoryId, result[1].CategoryId);
            Assert.Equal(product2.Category.Id, result[1].Category.Id);
            Assert.Equal(product2.Category.Name, result[1].Category.Name);
        }

        [Fact]
        public async void AddAsync()
        {
            var _repository = GetProductRepository();
            var item = new ProductItemBuilder().WithDefaultValues().Build();
            var product = await _repository.AddAsync(item);

            Assert.Equal(item.Id, product.Id);
            Assert.Equal(item.Name, product.Name);
            Assert.Equal(item.Description, product.Description);
            Assert.Equal(item.CategoryId, product.CategoryId);
            Assert.Equal(item.Category.Id, product.Category.Id);
            Assert.Equal(item.Category.Name, product.Category.Name);
        }

        [Fact]
        public async void UpdateAsync()
        {
            var _repository = GetProductRepository();

            var item = new ProductItemBuilder().WithDefaultValues().Build();

            var product = await _repository.AddAsync(item);


            string newName = "Test Product 1 Update";
            string newDescription = "Test Product 1 Description Update";

            product.Name = newName;
            product.Description = newDescription;

            await _repository.UpdateAsync(product);

            var updatedProduct = await _repository.GetByIdAsync(e => e.Id == item.Id, r => r.Category);

            Assert.Equal(newName, updatedProduct.Name);
            Assert.Equal(newDescription, updatedProduct.Description);
        }

        [Fact]
        public async void DeleteAsync()
        {
            var _repository = GetProductRepository();
            var item = new ProductItemBuilder().WithDefaultValues().Build();
            var product = await _repository.AddAsync(item);

            await _repository.DeleteAsync(product.Id);

            var deletedProduct = await _repository.GetByIdAsync(e => e.Id == item.Id, r => r.Category);

            Assert.Null(deletedProduct);
        }
    }
}