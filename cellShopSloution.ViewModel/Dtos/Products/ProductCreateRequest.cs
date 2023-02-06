using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace cellShopSolution.ViewModel.Dtos.Products
{
    public class ProductCreateRequest
    {

        public decimal Price { set; get; }
        public decimal OriginaPrice { set; get; }
        public int Stock { set; get; }
        [Required(ErrorMessage = "Bạn phải nhập tên sản phẩm")]
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public bool? IsFeatured { get; set; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }
        public IFormFile ProductslImage { get; set; }
    }
}
