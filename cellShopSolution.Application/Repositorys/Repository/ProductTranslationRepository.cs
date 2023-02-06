using cellShopSolution.Application.Repositorys.IRepository;
using cellShopSolution.Data.EF;
using cellShopSolution.Data.Entities;
using cellShopSolution.ViewModel.Dtos.Products;
using Microsoft.EntityFrameworkCore;

namespace cellShopSolution.Application.Repositorys.Repository
{
    public class ProductTranslationRepository : GenericRepository<ProductTranslation>, IProductTranslationRepository
    {
        public ProductTranslationRepository(cellShopDbContext context) : base(context)
        {
        }

        public async Task<ProductTranslation> GetProductTranslationAsync(int productId, string languageId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.ProductId == productId && x.LanguageId == languageId);
        }

        public async Task<ProductTranslation> UpdateProductTranslationAsync(ProductUpdateRequest updateRequest)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.ProductId == updateRequest.Id && x.LanguageId == updateRequest.LanguageId);
        }
    }
}
