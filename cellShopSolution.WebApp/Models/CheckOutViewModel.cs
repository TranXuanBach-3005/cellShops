using cellShopSloution.ViewModel.Dtos.Cart;

namespace cellShopSolution.WebApp.Models
{
	public class CheckOutViewModel
	{
		public List<CartItemViewModel> CartItems { get; set; }
		public CheckOutRequest CheckOutModel { get; set; }
	}
}
