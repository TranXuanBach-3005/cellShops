using cellShopSloution.ViewModel.Dtos.Slides;

namespace cellShopSolution.ApiIntegration.Services.IService
{
    public interface ISlideClient
    {
        Task<List<SlideViewModel>> GetAllSlideAsync();
    }
}
