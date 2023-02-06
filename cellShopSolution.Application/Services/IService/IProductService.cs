using cellShopSloution.ViewModel.Dtos.ProductImage;
using cellShopSloution.ViewModel.Dtos.Products;
using cellShopSloution.ViewModel.Dtos.Responses;
using cellShopSolution.ViewModel.Dtos;
using cellShopSolution.ViewModel.Dtos.Products;

namespace cellShopSolution.Application.Services.IService
{
    public interface IProductService
    {
        Task<PageResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest productPagingRequest);
        Task<PageResult<ProductViewModel>> GettAllPagingAsync(GetProductPagingRequest pagingRequest);
        Task<int> CreateProductAsync(ProductCreateRequest createRequest);
        Task<ProductUpdateRequest> UpdateProductAsync(int productId, ProductUpdateRequest updateRequest);
        Task<ProductViewModel> GetByIdProductAsync(int productId, string languageId);
        Task<bool> UpdatePriceAsync(int productId, decimal newPrice);
        Task<bool> UpdateStockAsync(int productId, int addQuantity);
        Task<int> DeleteProductAsync(int productId);
        Task AddViewCount(int productId);
        Task<int> CreateImageAsync(int productId, ProductImageCreateRequest productImageCreateRequest);
        Task<int> UpdateImageAsync(int imageId, ProductImageUpdateRequest productImageUpdateRequest);
        Task<int> DeleteImageAsync(int imageId);
        Task<List<ProductImageViewModel>> GetListImage(int productId);
        Task<ApiResponse<bool>> CategoryAssignAsync(int id, CategoryAssignRequest categoryAssign);
        Task<List<ProductViewModel>> GetListFeatured(string languageId, int take);
        Task<List<ProductViewModel>> GetListLatest(string languageId, int take);
        Task<List<ProductViewModel>> GetListRelated(string languageId, int take);
    }
}
