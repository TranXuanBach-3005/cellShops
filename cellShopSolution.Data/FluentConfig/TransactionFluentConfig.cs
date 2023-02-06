using cellShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cellShopSolution.Data.FluentConfig
{
    public class TransactionFluentConfig : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id).UseIdentityColumn();
            builder.HasOne(u => u.User)
                   .WithMany(t => t.Transactions)
                   .HasForeignKey(u => u.UserId);
        }
    }
}
