using cellShopSolution.ManagerApp.Models;
using cellShopSolution.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cellShopSolution.ManagerApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
        [HttpPost]
        public IActionResult Language(NavbarViewModel navbarView)
        {
            HttpContext.Session.SetString(SystemConstant.AppSettings.DefaultLanguageId, navbarView.CurrenLanguageId);
            return Redirect(navbarView.ReturnUrl);
        }
    }
}