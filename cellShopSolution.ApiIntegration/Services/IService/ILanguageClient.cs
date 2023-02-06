using cellShopSloution.ViewModel.Dtos.Languages;
using cellShopSloution.ViewModel.Dtos.Responses;

namespace cellShopSolution.ManagerApp.Services.IService
{
    public interface ILanguageClient
    {
        Task<ApiResponse<List<LanguageViewModel>>> GetAllLanguage();
    }
}
