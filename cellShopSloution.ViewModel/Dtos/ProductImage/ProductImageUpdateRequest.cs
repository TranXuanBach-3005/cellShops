using Microsoft.AspNetCore.Http;

namespace cellShopSloution.ViewModel.Dtos.ProductImage
{
    public class ProductImageUpdateRequest
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
