using cellShopSloution.ViewModel.Dtos.Categorys;
using cellShopSolution.ViewModel.Dtos.Products;
using cellShopSolution.ViewModel.Dtos;
using cellShopSloution.ViewModel.Dtos.ProductImage;

namespace cellShopSolution.WebApp.Models
{
	public class ProductDetailViewModel
	{
        public CategoryViewModel Category { get; set; }
        public ProductViewModel Product { get; set; }
        public List<ProductViewModel> RelatedProducts { get; set; }
        public List<ProductImageViewModel> ProductImages { get; set; }
    }
}
