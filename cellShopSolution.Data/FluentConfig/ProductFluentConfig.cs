using cellShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cellShopSolution.Data.FluentConfig
{
    public class ProductFluentConfig: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id).UseIdentityColumn();
            builder.Property(n => n.Price).IsRequired(true);
            builder.Property(n=>n.SeoAlias).IsRequired(false);
            builder.Property(n=>n.Stock).IsRequired(true);
            builder.Property(n => n.ViewCount).HasDefaultValue(0);
        }
    }
}
