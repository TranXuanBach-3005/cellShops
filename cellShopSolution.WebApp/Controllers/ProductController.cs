using cellShopSolution.ApiIntegration.Services.IService;
using cellShopSolution.ViewModel.Dtos.Products;
using cellShopSolution.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace cellShopSolution.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductClient _productClient;
        private readonly ICategoryClient _categoryClient;

        public ProductController(IProductClient productClient, ICategoryClient categoryClient)
        {
            _productClient = productClient;
            _categoryClient = categoryClient;
        }

        public async Task<IActionResult> ProductDetail(int productId, string culture)
        {
            var product = await _productClient.GetByIdProductAsync(productId, culture);
            return View(new ProductDetailViewModel()
            {
                Product = product,
                RelatedProducts = await _productClient.GetRelatedProducts(culture, 6)
            }); 
        }
        public async Task<IActionResult> ProductInCategory(string culture, int categoryId, int page = 1)
        {
            var products = await _productClient.GetProductsPagingAsync(new GetProductPagingRequest()
            {
                CategoryId = categoryId,
                PageIndex = page,
                LanguageId = culture,
                PageSize = 10
            });
            return View(new ProductCategoryViewModel()
            {
                Category = await _categoryClient.GetByCategoryAsync(culture,categoryId),
                Products = products
            }); 
        }
    }
}
