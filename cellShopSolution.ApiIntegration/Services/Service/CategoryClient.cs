using cellShopSloution.ViewModel.Dtos.Categorys;
using cellShopSolution.ApiIntegration.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace cellShopSolution.ApiIntegration.Services.Service
{
    public class CategoryClient : BaseClient, ICategoryClient
    {
        public CategoryClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
                          IHttpContextAccessor httpContextAccessor)
              : base(httpClientFactory, configuration, httpContextAccessor)
        {
        }
        public async Task<List<CategoryViewModel>> GetAllCategoryAsync(string languageId)
        {
               return await GetListAsync<CategoryViewModel>("api/categorys?languageId=" + languageId);
        }

        public async Task<CategoryViewModel> GetByCategoryAsync( string languageId, int categoryId)
        {
            return await GetAllAsync<CategoryViewModel>($"api/categorys/{languageId}/{categoryId}");
        }
    }
}
