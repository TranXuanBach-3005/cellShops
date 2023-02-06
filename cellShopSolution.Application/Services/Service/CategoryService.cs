using cellShopSloution.ViewModel.Dtos.Categorys;
using cellShopSolution.Application.Services.IService;
using cellShopSolution.Application.UnitOfworks;
using System.Collections.Immutable;

namespace cellShopSolution.Application.Services.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<CategoryViewModel>> GetAllCategoryAsync(string languageId)
        {
            var categoryList = await _unitOfWork.CategoryRepository.GetListCategory(languageId);
            return categoryList.Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                Name = x.CategoryTranslations[0].Name,
                ParentId = x.ParentId
            }).ToList();
        }

        public async Task<CategoryViewModel> GetByCategoryAsync(  string languageId, int categoryId)
        {
            var category = await _unitOfWork.CategoryRepository.GetByCategory( languageId, categoryId);
            return new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.CategoryTranslations[0].Name,
                ParentId=category.ParentId
            };
        }
    }
}
