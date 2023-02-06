using cellShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cellShopSolution.Data.FluentConfig
{
    public class UserFluentConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(n=>n.LastName).IsRequired();
            builder.Property(n=>n.FistName).IsRequired();
            builder.Property(n=>n.BirthDay).IsRequired();
        }
    }
}
