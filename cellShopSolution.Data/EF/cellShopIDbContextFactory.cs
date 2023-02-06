using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace cellShopSolution.Data.EF
{
    public class cellShopIDbContextFactory : IDesignTimeDbContextFactory<cellShopDbContext>
    {
        public cellShopDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("cellShopDb");
            var optionsBuiler = new DbContextOptionsBuilder<cellShopDbContext>();
            optionsBuiler.UseSqlServer(connectionString);
            return new cellShopDbContext(optionsBuiler.Options);
        }
    }
}
