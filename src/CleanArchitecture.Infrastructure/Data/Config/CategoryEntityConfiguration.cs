using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Category entity configuration.
/// </summary>

namespace CleanArchitecture.Infrastructure.Data.Config
{
    class CategoryEntityConfiguration
    {
        public void Configure(ModelBuilder builder)
        {
            builder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
