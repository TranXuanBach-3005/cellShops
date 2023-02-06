using cellShopSloution.ViewModel.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace cellShopSolution.WebApp.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PageResultBase resultBase)
        {
            return Task.FromResult((IViewComponentResult)View("Default", resultBase));
        }
    }
}
