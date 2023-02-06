using cellShopSolution.ApiIntegration.Services.IService;
using cellShopSolution.WebApp.Models;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace cellShopSolution.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISharedCultureLocalizer _loc;
        private readonly ISlideClient _slideClient;
        private readonly IProductClient _productClient;
        public HomeController(ILogger<HomeController> logger, ISharedCultureLocalizer loc,
            ISlideClient slideClient, IProductClient productClient)
        {
            _logger = logger;
            _loc = loc;
            _slideClient = slideClient;
            _productClient = productClient;
        }

        public async Task<IActionResult> Index()
        {
            var culture = CultureInfo.CurrentCulture.Name;
            var homeViewModel = new HomeViewModel()
            {
                Slides = await _slideClient.GetAllSlideAsync(),
                FeaturedProducts = await _productClient.GetFeaturedProducts(culture, 4),
                LatestProducts = await _productClient.GetLatestProducts(culture, 6)
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult SetCultureCookie(string cltr, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

            return LocalRedirect(returnUrl);
        }
    }
}