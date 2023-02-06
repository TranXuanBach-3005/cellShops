using cellShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cellShopSolution.Data.FluentConfig
{
    public class ProductTranslationFluentConfig : IEntityTypeConfiguration<ProductTranslation>
    {
        public void Configure(EntityTypeBuilder<ProductTranslation> builder)
        {
            builder.ToTable("ProductTranslations");
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id).UseIdentityColumn();
            builder.Property(n => n.Name).IsRequired();
            builder.Property(n => n.SeoAlias).IsRequired().HasMaxLength(500);
            builder.Property(n => n.LanguageId).IsUnicode(false).IsRequired();
            builder.HasOne(n => n.Language)
                   .WithMany(x => x.ProductTranslations)
                   .HasForeignKey(x => x.LanguageId);
            builder.HasOne(n => n.Product)
                   .WithMany(x => x.ProductTranslations)
                   .HasForeignKey(x => x.ProductId);
        }
    }
}
