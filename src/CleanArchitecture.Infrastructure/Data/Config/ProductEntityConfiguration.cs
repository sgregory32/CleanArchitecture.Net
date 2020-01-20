using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Product entity configuration.
/// </summary>

namespace CleanArchitecture.Infrastructure.Data.Config
{
    class ProductEntityConfiguration
    {
        public void Configure(ModelBuilder builder)
        {
            builder.Entity<Product>(entity =>
            {
                builder.Entity<Product>(entity =>
                {
                    entity.Property(e => e.Description).HasMaxLength(50);

                    entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.HasOne(d => d.Category)
                        .WithMany(p => p.Product)
                        .HasForeignKey(d => d.CategoryId)
                        .HasConstraintName("FK_Product_Category");
                });
            });
        }
    }
}
