using cellShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cellShopSolution.Data.FluentConfig
{
    public class CategoryTranslationFluentConfig : IEntityTypeConfiguration<CategoryTranslation>
    {
        public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
        {
            builder.ToTable("CategoryTranslations");
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id).UseIdentityColumn();
            builder.Property(n => n.Name).IsRequired().HasMaxLength(200);
            builder.Property(n => n.SeoAlias).IsRequired(true);
            builder.Property(n => n.SeoTitle).HasMaxLength(300);
            builder.Property(n => n.SeoDescription).HasMaxLength(600);
            builder.Property(n => n.LanguageId).IsUnicode(false).IsRequired();
            builder.HasOne(l => l.Language)
                   .WithMany(cl => cl.CategoryTranslations)
                   .HasForeignKey(l => l.LanguageId);
            builder.HasOne(c => c.Category)
                   .WithMany(cl => cl.CategoryTranslations)
                   .HasForeignKey(c => c.CategoryId);
        }
    }
}
