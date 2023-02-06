using cellShopSloution.ViewModel.Dtos.OrderDetail;

namespace cellShopSloution.ViewModel.Dtos.Cart
{
    public class CheckOutRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<OrderDetailViewModel> OrderDetailViews { get; set; } = new List<OrderDetailViewModel>();
    }
}
