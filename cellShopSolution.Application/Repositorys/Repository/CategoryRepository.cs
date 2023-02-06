using cellShopSolution.Application.Repositorys.IRepository;
using cellShopSolution.Data.EF;
using cellShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace cellShopSolution.Application.Repositorys.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(cellShopDbContext context) : base(context)
        {
        }

        public async Task<Category> GetByCategory(string languageId, int categoryId)
        {
            var result = await _dbSet.Where(x => x.Id == categoryId)
                .Include(x => x.CategoryTranslations.Where(y => y.LanguageId == languageId && y.CategoryId == categoryId))
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<Category>> GetListCategory(string languageId)
        {
            return await _dbSet.Include(x => x.CategoryTranslations
                                        .Where(y => y.LanguageId == languageId))
                                        .ToListAsync();
                                        
        }
    }
}
