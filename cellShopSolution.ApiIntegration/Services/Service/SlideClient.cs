using cellShopSloution.ViewModel.Dtos.Responses;
using cellShopSloution.ViewModel.Dtos.Slides;
using cellShopSolution.ApiIntegration.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace cellShopSolution.ApiIntegration.Services.Service
{
    public class SlideClient : BaseClient, ISlideClient
    {
        public SlideClient(IHttpClientFactory httpClientFactory,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, configuration, httpContextAccessor)
        {
        }

        public async Task<List<SlideViewModel>> GetAllSlideAsync()
        {
            return await GetAllAsync<List<SlideViewModel>>("api/slides");
        }
    }
}
