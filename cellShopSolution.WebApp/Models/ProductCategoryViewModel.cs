using cellShopSloution.ViewModel.Dtos.Categorys;
using cellShopSolution.ViewModel.Dtos;
using cellShopSolution.ViewModel.Dtos.Products;

namespace cellShopSolution.WebApp.Models
{
    public class ProductCategoryViewModel
    {
        public CategoryViewModel Category { get; set; }

        public PageResult<ProductViewModel> Products { get; set; }
    }
}
