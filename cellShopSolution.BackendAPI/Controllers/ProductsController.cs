using cellShopSloution.ViewModel.Dtos.ProductImage;
using cellShopSloution.ViewModel.Dtos.Products;
using cellShopSolution.Application.Services.IService;
using cellShopSolution.ViewModel.Dtos.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cellShopSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllProductPaging([FromQuery] GetProductPagingRequest pagingRequest)
        {
            var products = await _productService.GettAllPagingAsync(pagingRequest);
            return Ok(products);
        }
        [HttpGet("featured/{languageId}/{take}")]
        public async Task<IActionResult> GetFeaturedProducts(string languageId, int take)
        {
            var featuredProduct = await _productService.GetListFeatured(languageId, take);
            return Ok(featuredProduct);
        }
        [HttpGet("latest/{languageId}/{take}")]
        public async Task<IActionResult> GetLatestProducts(string languageId, int take)
        {
            var latestProduct = await _productService.GetListLatest(languageId, take);
            return Ok(latestProduct);
        }
        [HttpGet("related/{languageId}/{take}")]
        public async Task<IActionResult> GetRelatedProducts(string languageId, int take)
        {
            var latestProduct = await _productService.GetListRelated(languageId, take);
            return Ok(latestProduct);
        }
        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetByIdProduct(int productId, string languageId)
        {
            var product = await _productService.GetByIdProductAsync(productId, languageId);
            return Ok(product);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateRequest productCreateRequest)
        {
            var productId = await _productService.CreateProductAsync(productCreateRequest);
            if (productId == 0) return BadRequest();
            var product = await _productService.GetByIdProductAsync(productId, productCreateRequest.LanguageId);
            return Ok(product);
        }
        [HttpPut("{productId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int productId, [FromForm] ProductUpdateRequest productUpdateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            productUpdateRequest.Id = productId;
            var result = await _productService.UpdateProductAsync(productId, productUpdateRequest);
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [HttpPatch("price/{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePriceProduct(int productId, decimal newPrice)
        {
            var result = await _productService.UpdatePriceAsync(productId, newPrice);
            if (result == false) return BadRequest();
            return Ok(result);
        }
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _productService.DeleteProductAsync(productId);
            if (result == 0) return BadRequest();
            return Ok(result);
        }
        //Images
        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest productImageCreate)
        {
            var result = await _productService.CreateImageAsync(productId, productImageCreate);
            return Ok(result);
        }
        [HttpPut("images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest productImageUpdate)
        {
            var result = await _productService.UpdateImageAsync(imageId, productImageUpdate);
            if (result == 0) return BadRequest();
            return Ok(result);
        }
        [HttpPut("{id}/category")]
        public async Task<IActionResult> CategoryAssign(int id, [FromBody] CategoryAssignRequest categoryAssign)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _productService.CategoryAssignAsync(id, categoryAssign);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
