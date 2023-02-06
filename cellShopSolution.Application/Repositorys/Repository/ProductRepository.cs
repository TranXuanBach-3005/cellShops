using cellShopSolution.Application.Repositorys.IRepository;
using cellShopSolution.Data.EF;
using cellShopSolution.Data.Entities;
using cellShopSolution.ViewModel.Dtos.Products;
using Microsoft.EntityFrameworkCore;

namespace cellShopSolution.Application.Repositorys.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
       
        public ProductRepository(cellShopDbContext context) : base(context)
        {
            
        }

        //public async Task<IQueryable<string>> GetAllPaging(GetProductPagingRequest pagingRequest)
        //{
        //    var query = await (from p in _dbContext.Products
        //                       join pt in _dbContext.ProductTranslations on p.Id equals pt.ProductId
        //                       join pic in _dbContext.ProductCategories on p.Id equals pic.ProductId into ppic
        //                       from pic in ppic.DefaultIfEmpty()
        //                       join c in _dbContext.Categories on pic.CategoryId equals c.Id into picc
        //                       from c in ppic.DefaultIfEmpty()
        //                       join pi in _dbContext.ProductImages on p.Id equals pi.ProductId into ppi
        //                       from pi in ppi.DefaultIfEmpty()
        //                       where pt.LanguageId == pagingRequest.LanguageId && pi.IsDefault == true
        //                       select new { p, pt, pic, c, pi }).ToListAsync();
        //    return query;
        //}
        public async Task<List<Product>> GetAllProductAsync(int productId)
        {
            return await _dbSet.Include(x => x.ProductTranslations).Where(x=>x.Id == productId)
                               .ToListAsync();
        }
    }
}
