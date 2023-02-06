using cellShopSolution.ManagerApp.Models;
using cellShopSolution.ManagerApp.Services.IService;
using cellShopSolution.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;

namespace cellShopSolution.ManagerApp.Components
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly ILanguageClient _languageClient;

        public NavbarViewComponent(ILanguageClient languageClient)
        {
            _languageClient = languageClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languages = await _languageClient.GetAllLanguage();
            var navbarViewModel = new NavbarViewModel()
            {
                CurrenLanguageId = HttpContext.Session
                                  .GetString(SystemConstant.AppSettings.DefaultLanguageId),
                Languages = languages.ResultObj

            };
            return View("Default", navbarViewModel);
        }
    }
}
