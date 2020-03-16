using CleanArchitecture.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CleanArchitecture.FunctionalTests
{
    public class AppDbContext<TStartup> : WebApplicationFactory<Api.TStartup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var dbContext = scopedServices.GetRequiredService<AppDbContext>();

                    dbContext.Database.EnsureCreated();

                    try
                    {
                        SeedData.PopulateTestData(dbContext);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("An error occurred seeding the " + $"database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }
}
