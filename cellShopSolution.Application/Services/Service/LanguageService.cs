using cellShopSloution.ViewModel.Dtos.Languages;
using cellShopSloution.ViewModel.Dtos.Responses;
using cellShopSolution.Application.Services.IService;
using Microsoft.EntityFrameworkCore;
using cellShopSolution.Application.UnitOfworks;

namespace cellShopSolution.Application.Services.Service
{
    public class LanguageService : ILanguageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LanguageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<List<LanguageViewModel>>> GetAllLanguageAsync()
        {
            var languages = await _unitOfWork.LanguageRepository.GetAllAsync();
            var languageViewModel = languages.Select(x => new LanguageViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return new ApiSuccessResponse<List<LanguageViewModel>>(languageViewModel);
        }
    }
}
