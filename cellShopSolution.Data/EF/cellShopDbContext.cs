using cellShopSolution.Data.Entities;
using cellShopSolution.Data.Extensions;
using cellShopSolution.Data.FluentConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace cellShopSolution.Data.EF
{
    public class cellShopDbContext : IdentityDbContext<User, Role, Guid>
    {
        public cellShopDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            modelBuilder.ApplyConfiguration(new AppConfigFluentConfig());
            modelBuilder.ApplyConfiguration(new ProductFluentConfig());
            modelBuilder.ApplyConfiguration(new CategoryFluentConfig());
            modelBuilder.ApplyConfiguration(new ProductImageFluentConfig());
            modelBuilder.ApplyConfiguration(new ProductInCategoryFluentConfig());
            modelBuilder.ApplyConfiguration(new ProductTranslationFluentConfig());
            modelBuilder.ApplyConfiguration(new OrderFluentConfig());
            modelBuilder.ApplyConfiguration(new OrderDetailFluentConfig());
            modelBuilder.ApplyConfiguration(new LanguageFluentConfig());
            modelBuilder.ApplyConfiguration(new ContactFluentConfig());
            modelBuilder.ApplyConfiguration(new ProductTranslationFluentConfig());
            modelBuilder.ApplyConfiguration(new CategoryTranslationFluentConfig());
            modelBuilder.ApplyConfiguration(new PromotionFluentConfig());
            modelBuilder.ApplyConfiguration(new SlideFluenConfig());
            modelBuilder.ApplyConfiguration(new TransactionFluentConfig());


            modelBuilder.ApplyConfiguration(new UserFluentConfig());
            modelBuilder.ApplyConfiguration(new RoleFluentConfig());
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles").HasKey(n=> new {n.UserId, n.RoleId});
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins").HasKey(n=>n.UserId);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens").HasKey(n=>n.UserId);
            //base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppConfig> AppConfigs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public DbSet<ProductInCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Slide> Slides { get; set; }
    }
}
