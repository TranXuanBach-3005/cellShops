using cellShopSolution.Application.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cellShopSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategorysController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategory(string languageId)
        {
            var categorys = await _categoryService.GetAllCategoryAsync(languageId);
            return Ok(categorys);
        }
        [HttpGet("{languageId}/{categoryId}")]
        public async Task<IActionResult> GetByCategory(string languageId, int categoryId)
        {
            var categorys = await _categoryService.GetByCategoryAsync(languageId, categoryId);
            return Ok(categorys);
        }
    }
}
