using cellShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cellShopSolution.Data.FluentConfig
{
    public class RoleFluentConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.Property(n => n.Description).IsRequired().HasMaxLength(300);
        }
    }
}
