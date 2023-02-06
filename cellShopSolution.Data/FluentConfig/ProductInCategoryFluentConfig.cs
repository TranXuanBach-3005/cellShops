using cellShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cellShopSolution.Data.FluentConfig
{
    public class ProductInCategoryFluentConfig : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.HasKey(pc => new
            {
                pc.ProductId,
                pc.CategoryId
            });
            builder.ToTable("ProductInCategories");
            builder.HasOne(p=>p.Product)
                   .WithMany(pc=>pc.ProductInCategories)
                   .HasForeignKey(pc=>pc.ProductId);

            builder.HasOne(c => c.Category)
                   .WithMany(pc => pc.ProductInCategories)
                   .HasForeignKey(pc => pc.CategoryId);

        }
    }
}
