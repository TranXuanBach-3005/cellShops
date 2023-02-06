using cellShopSloution.ViewModel.Dtos.Products;
using cellShopSolution.Application.Repositorys.IRepository;
using cellShopSolution.Data.EF;
using cellShopSolution.Data.Entities;

namespace cellShopSolution.Application.Repositorys.Repository
{
    public class ProductInCategoryRepository : GenericRepository<ProductInCategory>, IProductInCategoryRepository
    {
        public ProductInCategoryRepository(cellShopDbContext context) : base(context)
        {
            
        }
    }
}
