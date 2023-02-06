using cellShopSloution.ViewModel.Dtos.ProductImage;
using cellShopSolution.Application.Repositorys.IRepository;
using cellShopSolution.Data.EF;
using cellShopSolution.Data.Entities;
using cellShopSolution.ViewModel.Dtos.Products;
using Microsoft.EntityFrameworkCore;

namespace cellShopSolution.Application.Repositorys.Repository
{
    public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(cellShopDbContext context) : base(context)
        {
        }

        public  async Task<List<ProductImage>> DeleteProductImageAsync(int productId)
        {
            return await _dbSet.Where(x => x.ProductId == productId).ToListAsync();
        }

        public async Task<ProductImage> GetByIdProductImamge(int productId)
        {
            return await _dbSet.Where(x => x.ProductId == productId && x.IsDefault == true).FirstOrDefaultAsync();
        }

        public async Task<List<ProductImage>> GetProductImageAsync(int productId)
        {
            return await _dbSet.Include(x=>x.Product)
                               .Where(x => x.ProductId == productId).ToListAsync();
        }

        public async Task<ProductImage> UpdateProductImageAsync(ProductUpdateRequest updateRequest)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.IsDefault == true && x.ProductId == updateRequest.Id);
        }
    }
}
