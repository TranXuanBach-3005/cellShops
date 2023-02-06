using cellShopSolution.Data.Entities;
using cellShopSolution.ViewModel.Dtos.Products;

namespace cellShopSolution.Application.Repositorys.IRepository
{
    public interface IProductTranslationRepository: IGenericRepository<ProductTranslation>
    {
        Task<ProductTranslation> UpdateProductTranslationAsync(ProductUpdateRequest updateRequest);
        Task<ProductTranslation> GetProductTranslationAsync(int productId, string languageId);
    }
}
