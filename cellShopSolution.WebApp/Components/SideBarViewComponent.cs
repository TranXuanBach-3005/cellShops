using cellShopSolution.ApiIntegration.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace cellShopSolution.WebApp.Components
{
    public class SideBarViewComponent:ViewComponent
    {
        private readonly ICategoryClient _categoryClient;

        public SideBarViewComponent(ICategoryClient categoryClient)
        {
            _categoryClient = categoryClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()                               
        {
            var items = await _categoryClient.GetAllCategoryAsync(CultureInfo.CurrentCulture.Name);
            return View(items);
        }

    }

}
