using cellShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cellShopSolution.Data.FluentConfig
{
    public class PromotionFluentConfig : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions");
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id).UseIdentityColumn();
            builder.Property(n => n.Name).IsRequired();
        }
    }
}
