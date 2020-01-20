using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.IntegrationTests
{
    public abstract class AsyncRepositoryTestFixture
    {
        protected AppDbContext _dbContext;

        protected static DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("cleanarchitecture")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected AsyncRepository<Category> GetCategoryRepository()
        {
            var options = CreateNewContextOptions();

            _dbContext = new AppDbContext(options);
            return new AsyncRepository<Category>(_dbContext);
        }

        protected AsyncRepository<Product> GetProductRepository()
        {
            var options = CreateNewContextOptions();

            _dbContext = new AppDbContext(options);
            return new AsyncRepository<Product>(_dbContext);
        }
    }
}
