using cellShopSloution.ViewModel.Dtos.ProductImage;
using cellShopSolution.Data.Entities;
using cellShopSolution.ViewModel.Dtos.Products;

namespace cellShopSolution.Application.Repositorys.IRepository
{
    public interface IProductImageRepository:IGenericRepository<ProductImage>
    {
        Task<ProductImage> UpdateProductImageAsync(ProductUpdateRequest updateRequest);
        Task<List<ProductImage>> GetProductImageAsync(int productId);
        Task<List<ProductImage>> DeleteProductImageAsync(int productId);
        Task<ProductImage> GetByIdProductImamge(int productId);
    }
}
