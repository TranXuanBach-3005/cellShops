using cellShopSloution.ViewModel.Dtos.Slides;

namespace cellShopSolution.Application.Services.IService
{
    public interface ISlideService
    {
        Task<List<SlideViewModel>> GetAllSlides();
    }
}
