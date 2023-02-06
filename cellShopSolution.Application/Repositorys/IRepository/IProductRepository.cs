using cellShopSolution.Data.Entities;
using cellShopSolution.ViewModel.Dtos.Products;

namespace cellShopSolution.Application.Repositorys.IRepository
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        //IQueryable GetAllPaging(GetProductPagingRequest pagingRequest);
        Task<List<Product>> GetAllProductAsync(int productId);
    }
}
