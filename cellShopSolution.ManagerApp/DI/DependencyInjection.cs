using cellShopSloution.ViewModel.FluentValidation;
using cellShopSolution.ApiIntegration.Services.IService;
using cellShopSolution.ApiIntegration.Services.Service;
using cellShopSolution.ManagerApp.Services.IService;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace cellShopSolution.ManagerApp.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddManagerAppService(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Login/Index/";
                        options.AccessDeniedPath = "/Users/Forbidden/";
                    });
            services.AddControllersWithViews()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //
            services.AddScoped<IUserClient, UserClient>();
            services.AddScoped<IRoleClient, RoleClient>();
            services.AddScoped<ILanguageClient, LanguageClient>();
            services.AddScoped<IProductClient, ProductClient>();
            services.AddScoped<ICategoryClient, CategoryClient>();
            return services;
        }
    }
}
