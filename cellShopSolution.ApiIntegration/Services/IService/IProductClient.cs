using cellShopSloution.ViewModel.Dtos.Products;
using cellShopSloution.ViewModel.Dtos.Responses;
using cellShopSolution.ViewModel.Dtos;
using cellShopSolution.ViewModel.Dtos.Products;

namespace cellShopSolution.ApiIntegration.Services.IService
{
    public interface IProductClient
    {
        Task<PageResult<ProductViewModel>> GetProductsPagingAsync(GetProductPagingRequest getProductPaging);
        Task<bool> CreateProductAsync(ProductCreateRequest productCreate);
        Task<ApiResponse<bool>> UpdateProductAsync(ProductUpdateRequest productUpdate);
        Task<bool> DelectProductAsync(int productId);
        Task<ProductViewModel> GetByIdProductAsync(int Id, string languageId);
        Task<List<ProductViewModel>> GetFeaturedProducts(string languageId, int take);
        Task<List<ProductViewModel>> GetLatestProducts(string languageId, int take);
        Task<List<ProductViewModel>> GetRelatedProducts(string languageId, int take);
        Task<ApiResponse<bool>> CategoryAssignAsync(int id, CategoryAssignRequest categoryAssign);
    }
}
