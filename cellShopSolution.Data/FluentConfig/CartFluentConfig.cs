using cellShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cellShopSolution.Data.FluentConfig
{
    public class CartFluentConfig : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id).UseIdentityColumn();
            builder.Property(n => n.Price).IsRequired();
            builder.Property(n => n.Quantity).IsRequired();
            builder.HasOne(p => p.Product)
                   .WithMany(c => c.Carts)
                   .HasForeignKey(p => p.ProductId);
            builder.HasOne(u => u.User)
                   .WithMany(c => c.Carts)
                   .HasForeignKey(u => u.UserId);
        }
    }
}
