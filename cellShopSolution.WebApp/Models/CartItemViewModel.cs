namespace cellShopSolution.WebApp.Models
{
    public class CartItemViewModel
    {
        public int productId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
}
