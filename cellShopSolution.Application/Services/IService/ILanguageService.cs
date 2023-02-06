using cellShopSloution.ViewModel.Dtos.Languages;
using cellShopSloution.ViewModel.Dtos.Responses;

namespace cellShopSolution.Application.Services.IService
{
    public interface ILanguageService
    {
        Task<ApiResponse<List<LanguageViewModel>>> GetAllLanguageAsync();
    }
}
