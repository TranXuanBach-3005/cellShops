using cellShopSloution.ViewModel.Dtos.Products;
using cellShopSloution.ViewModel.Dtos.Responses;
using cellShopSolution.ApiIntegration.Services.IService;
using cellShopSolution.Utilities.Constants;
using cellShopSolution.ViewModel.Dtos;
using cellShopSolution.ViewModel.Dtos.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace cellShopSolution.ApiIntegration.Services.Service
{
    public class ProductClient : BaseClient, IProductClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public ProductClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
                          IHttpContextAccessor httpContextAccessor)
                           : base(httpClientFactory, configuration, httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<ApiResponse<bool>> CategoryAssignAsync(int id, CategoryAssignRequest categoryAssign)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var jonData = JsonConvert.SerializeObject(categoryAssign);
            var content = new StringContent(jonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"/api/Products/{id}/category", content);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResponse<bool>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResponse<bool>>(result);

        }

        public async Task<bool> CreateProductAsync(ProductCreateRequest productCreate)
        {
            var sessions = _httpContextAccessor
               .HttpContext
               .Session
               .GetString(SystemConstant.AppSettings.Token);

            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSettings.DefaultLanguageId);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstant.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (productCreate.ProductslImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(productCreate.ProductslImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)productCreate.ProductslImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ProductslImage", productCreate.ProductslImage.FileName);
            }

            requestContent.Add(new StringContent(productCreate.Price.ToString()), "price");
            requestContent.Add(new StringContent(productCreate.OriginaPrice.ToString()), "originalPrice");
            requestContent.Add(new StringContent(productCreate.Stock.ToString()), "stock");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(productCreate.Name) ? "" : productCreate.Name.ToString()), "name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(productCreate.Description) ? "" : productCreate.Description.ToString()), "description");

            requestContent.Add(new StringContent(string.IsNullOrEmpty(productCreate.Details) ? "" : productCreate.Details.ToString()), "details");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(productCreate.SeoDescription) ? "" : productCreate.SeoDescription.ToString()), "seoDescription");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(productCreate.SeoTitle) ? "" : productCreate.SeoTitle.ToString()), "seoTitle");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(productCreate.SeoAlias) ? "" : productCreate.SeoAlias.ToString()), "seoAlias");
            requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PostAsync($"/api/Products/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DelectProductAsync(int productId)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.DeleteAsync($"/api/Products/{productId}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(result);
            }
            return JsonConvert.DeserializeObject<bool>(result);
        }

        public async Task<ProductViewModel> GetByIdProductAsync(int Id, string languageId)
        {
            var productData = await GetAllAsync<ProductViewModel>($"api/products/{Id}/{languageId}");
            return productData;
        }

        public async Task<List<ProductViewModel>> GetFeaturedProducts(string languageId, int take)
        {
            var featuredProducts = await GetListAsync<ProductViewModel>($"/api/products/featured/{languageId}/{take}");
            return featuredProducts;
        }

        public async Task<List<ProductViewModel>> GetLatestProducts(string languageId, int take)
        {
            var latestProducts = await GetListAsync<ProductViewModel>($"/api/products/latest/{languageId}/{take}");
            return latestProducts;
        }

        public async Task<PageResult<ProductViewModel>> GetProductsPagingAsync(GetProductPagingRequest getProductPaging)
        {
            var productData = await GetAllAsync<PageResult<ProductViewModel>>
                ($"/api/products/paging?pageIndex={getProductPaging.PageIndex}" +
                $"&pageSize={getProductPaging.PageSize}" +
                $"&keyword={getProductPaging.Keyword}&languageId={getProductPaging.LanguageId}&categoryId={getProductPaging.CategoryId}");

            return productData;
        }

        public async Task<List<ProductViewModel>> GetRelatedProducts(string languageId, int take)
        {
            var relatedProducts = await GetListAsync<ProductViewModel>($"/api/products/related/{languageId}/{take}");
            return relatedProducts;
        }

        public async Task<ApiResponse<bool>> UpdateProductAsync(ProductUpdateRequest productUpdate)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSettings.DefaultLanguageId);
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var jonData = JsonConvert.SerializeObject(productUpdate);
            var requestContent = new MultipartFormDataContent();

            if (productUpdate.ProductslImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(productUpdate.ProductslImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)productUpdate.ProductslImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ProductslImage", productUpdate.ProductslImage.FileName);
            }
            requestContent.Add(new StringContent(string.IsNullOrEmpty(productUpdate.Name) ? "" : productUpdate.Name.ToString()), "name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(productUpdate.Description) ? "" : productUpdate.Description.ToString()), "description");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(productUpdate.Details) ? "" : productUpdate.Details.ToString()), "details");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(productUpdate.SeoDescription) ? "" : productUpdate.SeoDescription.ToString()), "seoDescription");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(productUpdate.SeoTitle) ? "" : productUpdate.SeoTitle.ToString()), "seoTitle");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(productUpdate.SeoAlias) ? "" : productUpdate.SeoAlias.ToString()), "seoAlias");
            requestContent.Add(new StringContent(languageId), "languageId");
            var response = await client.PutAsync($"/api/products/" + productUpdate.Id, requestContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResponse<bool>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResponse<bool>>(result);
        }
    }
}
