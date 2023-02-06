using cellShopSloution.ViewModel.Dtos;
using cellShopSloution.ViewModel.Dtos.Products;
using cellShopSolution.ApiIntegration.Services.IService;
using cellShopSolution.Utilities.Constants;
using cellShopSolution.ViewModel.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace cellShopSolution.ManagerApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductClient _productClient;
        private readonly IConfiguration _configuration;
        private readonly ICategoryClient _categoryClient;
        public ProductController(IProductClient productClient,
            IConfiguration configuration,
            ICategoryClient categoryClient)
        {
            _productClient = productClient;
            _configuration = configuration;
            _categoryClient = categoryClient;
        }
        public async Task<IActionResult> Index(string? keyword, int? categoryId, int pageIndex = 1, int pageSize = 10)
        {
            var languageId = HttpContext.Session.GetString(SystemConstant.AppSettings.DefaultLanguageId);
            var productRequest = new GetProductPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId,
                CategoryId = categoryId
            };
            var userData = await _productClient.GetProductsPagingAsync(productRequest);
            ViewBag.Keyword = keyword;

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMessage = TempData["result"];
            }
            var categorys = await _categoryClient.GetAllCategoryAsync(languageId);
            ViewBag.Category = categorys.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View(userData);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new ProductDeleteRequest()
            {
                Id = id
            });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ProductDeleteRequest producDelete)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _productClient.DelectProductAsync(producDelete.Id);
            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }
            return View(producDelete);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id )
        {
            var languageId =  HttpContext.Session.GetString(SystemConstant.AppSettings.DefaultLanguageId);
            var product = await _productClient.GetByIdProductAsync(id, languageId);
            var productViewModel = new ProductUpdateRequest()
            {
                Id = product.Id,
                Name = product.Name,
                Details = product.Details,
                Description = product.Description,
                SeoAlias = product.SeoAlias,
                SeoDescription = product.SeoDescription,
                SeoTitle = product.SeoTitle,
            };
            return View(productViewModel);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] ProductUpdateRequest productUpdate)
        {
            var result = await _productClient.UpdateProductAsync(productUpdate);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest productCreate)
        {
            var result = await _productClient.CreateProductAsync(productCreate);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }
            return View(productCreate);
        }
        [HttpGet]
        public async Task<IActionResult> CategoryAssign(int id)
        {
            var categoryAssign = await GetCategoryAsync(id);
            return View(categoryAssign);
        }
        [HttpPost]
        public async Task<IActionResult> CategoryAssign(CategoryAssignRequest categoryAssignRequest)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _productClient.CategoryAssignAsync(categoryAssignRequest.Id, categoryAssignRequest);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật danh mục thành công";
                return RedirectToAction("Index");
            }
            var categoryAssign = await GetCategoryAsync(categoryAssignRequest.Id);
            return View(categoryAssign);
        }
        private async Task<CategoryAssignRequest> GetCategoryAsync(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstant.AppSettings.DefaultLanguageId);

            var productResult = await _productClient.GetByIdProductAsync(id, languageId);
            var categorys = await _categoryClient.GetAllCategoryAsync(languageId);
            var categoryAssign = new CategoryAssignRequest();
            foreach (var role in categorys)
            {
                categoryAssign.Categories.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = productResult.Categories.Contains(role.Name)

                });
            }
            return categoryAssign;
        }
    }
}
