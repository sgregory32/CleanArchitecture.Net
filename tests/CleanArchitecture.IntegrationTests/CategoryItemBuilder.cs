using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.IntegrationTests
{
    public class CategoryItemBuilder
        {
            private Category _category = new Category();

            public CategoryItemBuilder Id(int id)
            {
                _category.Id = id;
                return this;
            }

            public CategoryItemBuilder Name(string name)
            {
                _category.Name = name;
                return this;
            }

            public CategoryItemBuilder WithDefaultValues()
            {
                _category = new Category() { Id = 1, Name = "Test Category 1" };
                return this;
            }

            public Category Build() => _category;
        }
}
