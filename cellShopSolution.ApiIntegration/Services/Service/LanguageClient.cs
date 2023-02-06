using cellShopSloution.ViewModel.Dtos.Languages;
using cellShopSloution.ViewModel.Dtos.Responses;
using cellShopSolution.ManagerApp.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace cellShopSolution.ApiIntegration.Services.Service
{
    public class LanguageClient : BaseClient, ILanguageClient
    {
        public LanguageClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
                          IHttpContextAccessor httpContextAccessor)
              : base(httpClientFactory, configuration, httpContextAccessor)
        {
        }
        public async Task<ApiResponse<List<LanguageViewModel>>> GetAllLanguage()
        {
            return await GetAllAsync<ApiResponse<List<LanguageViewModel>>>("api/languages");
        }
    }
}
