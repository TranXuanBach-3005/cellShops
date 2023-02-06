using cellShopSloution.ViewModel.Dtos.Categorys;
using cellShopSloution.ViewModel.Dtos.Responses;

namespace cellShopSolution.ApiIntegration.Services.IService
{
    public interface ICategoryClient
    {
        Task<List<CategoryViewModel>> GetAllCategoryAsync(string languageId);
        Task<CategoryViewModel> GetByCategoryAsync(string languageId, int categoryId);
    }
}
