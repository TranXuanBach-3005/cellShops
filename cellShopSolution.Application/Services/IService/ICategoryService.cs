using cellShopSloution.ViewModel.Dtos.Categorys;

namespace cellShopSolution.Application.Services.IService
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAllCategoryAsync(string languageId);
        Task<CategoryViewModel> GetByCategoryAsync(string languageId, int categoryId);
    }
}
