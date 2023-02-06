using cellShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cellShopSolution.Data.FluentConfig
{
    public class ProductImageFluentConfig : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id).UseIdentityColumn();
            builder.Property(n => n.Caption).IsRequired();
            builder.Property(n => n.ImagePath).IsRequired();
            builder.HasOne(p => p.Product)
                   .WithMany(pi => pi.ProductImages)
                   .HasForeignKey(p => p.ProductId);
        }
    }
}
