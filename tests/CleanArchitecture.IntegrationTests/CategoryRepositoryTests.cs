using Xunit;

namespace CleanArchitecture.IntegrationTests
{
    public class CategoryRepositoryTests : AsyncRepositoryTestFixture
    {
        [Fact]
        public async void GetByIdAsync()
        {
            var _repository = GetCategoryRepository();
            var item = new CategoryItemBuilder().WithDefaultValues().Build();
            await _repository.AddAsync(item);

            var category = await _repository.GetByIdAsync(e => e.Id == item.Id, r => r.Product);

            Assert.Equal(item.Id, category.Id);
            Assert.Equal(item.Name, category.Name);
        }

        [Fact]
        public async void ListAsync()
        {
            var _repository = GetCategoryRepository();
            var item = new CategoryItemBuilder().WithDefaultValues().Build();
            var category1 = await _repository.AddAsync(item);

            var newItem = new CategoryItemBuilder().Build();
            newItem.Name = "Test Category 2";
            var category2 = await _repository.AddAsync(newItem);

            var result = await _repository.ListAsync();

            Assert.Equal(category1.Id, result[0].Id);
            Assert.Equal(category1.Name, result[0].Name);
            Assert.True(result[1].Id > 0);
            Assert.Equal(category2.Name, result[1].Name);
        }

        [Fact]
        public async void AddAsync()
        {
            var _repository = GetCategoryRepository();
            var item = new CategoryItemBuilder().Build();
            item.Name = "Test Category 1";
            var category = await _repository.AddAsync(item);

            Assert.True(category.Id > 0);
            Assert.Equal(category.Name, item.Name);
        }

        [Fact]
        public async void UpdateAsync()
        {
            var _repository = GetCategoryRepository();
            var item = new CategoryItemBuilder().WithDefaultValues().Build();
            var category = await _repository.AddAsync(item);

            string newName = "Test Category 1 Update";
            category.Name = newName;
            await _repository.UpdateAsync(category);

            var updatedCategory = await _repository.GetByIdAsync(e => e.Id == category.Id, r => r.Product);

            Assert.Equal(newName, updatedCategory.Name);
        }

        [Fact]
        public async void DeleteAsync()
        {
            var _repository = GetCategoryRepository();
            var item = new CategoryItemBuilder().WithDefaultValues().Build();
            var category = await _repository.AddAsync(item);

            await _repository.DeleteAsync(category.Id);

            var deletedCategory = await _repository.GetByIdAsync(e => e.Id == category.Id, r => r.Product);

            Assert.Null(deletedCategory);
        }
    }
}
