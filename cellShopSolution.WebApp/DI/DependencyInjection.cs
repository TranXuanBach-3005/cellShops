using cellShopSloution.ViewModel.FluentValidation;
using cellShopSolution.ApiIntegration.Services.IService;
using cellShopSolution.ApiIntegration.Services.Service;
using cellShopSolution.WebApp.LocalizationResources;
using FluentValidation.AspNetCore;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace cellShopSolution.WebApp.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddManagerAppService(this IServiceCollection services)
        {
            services.AddHttpClient();
            var cultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("vi"),
            };
            services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>())
                .AddExpressLocalization<ExpressLocalizationResource, ViewLocalizationResource>(ops =>
                {
                    ops.UseAllCultureProviders = false;
                    ops.ResourcesPath = "LocalizationResources";
                    ops.RequestLocalizationOptions = o =>
                    {
                        o.SupportedCultures = cultures;
                        o.SupportedUICultures = cultures;
                        o.DefaultRequestCulture = new RequestCulture("vi");
                    };
                });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Account/Login/";
                        options.AccessDeniedPath = "/Users/Forbidden/";
                    });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ISlideClient, SlideClient>();
            services.AddScoped<IProductClient, ProductClient>();
            services.AddScoped<ICategoryClient, CategoryClient>();
            services.AddScoped<IUserClient, UserClient>();
            return services;
        }
    }
}
