using cellShopSloution.ViewModel.Dtos.Slides;
using cellShopSolution.ViewModel.Dtos.Products;

namespace cellShopSolution.WebApp.Models
{
    public class HomeViewModel
    {
        public List<SlideViewModel> Slides { get; set; }
        public List<ProductViewModel> FeaturedProducts { get; set; }
        public List<ProductViewModel> LatestProducts { get; set; }
    }
}
