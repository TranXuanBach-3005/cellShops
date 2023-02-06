using cellShopSolution.Application.Repositorys.IRepository;
using cellShopSolution.Application.Repositorys.Repository;
using cellShopSolution.Application.Services.IService;
using cellShopSolution.Application.Services.Service;
using cellShopSolution.Application.UnitOfworks;
using cellShopSolution.Data.EF;
using cellShopSolution.Data.Entities;
using cellShopSolution.Utilities.Constants;
using cellShopSolution.ViewModel.AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace cellShopSolution.Application.DI
{
    public static class DenpendencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(
           this IServiceCollection services,
           IConfiguration configuration,
           IWebHostEnvironment environment
           )
        {
            //DI
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISlideRepository, SlideRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IProductTranslationRepository, ProductTranslationRepository>();
            services.AddScoped<IProductInCategoryRepository, ProductInCategoryRepository>();
            services.AddScoped<UserManager<User>, UserManager<User>>();
            services.AddScoped<SignInManager<User>, SignInManager<User>>();
            services.AddScoped<RoleManager<Role>, RoleManager<Role>>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISlideService, SlideService>();
            services.AddScoped<ILanguageService, LanguageService>();
            //DbContext
            services.AddDbContext<cellShopDbContext>(options =>
                options.UseSqlServer(configuration
                       .GetConnectionString(SystemConstant.CellConnectionString)));
            //Mapper
            services.AddAutoMapper(typeof(Mapping).Assembly);
            //Identity
            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<cellShopDbContext>()
                    .AddDefaultTokenProviders();
            return services;
        }
    }
}
