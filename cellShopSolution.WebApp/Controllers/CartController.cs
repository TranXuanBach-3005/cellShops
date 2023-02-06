using cellShopSloution.ViewModel.Dtos.Cart;
using cellShopSloution.ViewModel.Dtos.OrderDetail;
using cellShopSolution.ApiIntegration.Services.IService;
using cellShopSolution.Utilities.Constants;
using cellShopSolution.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace cellShopSolution.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductClient _productClient;

        public CartController(IProductClient productClient)
        {
            _productClient = productClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetlistItmes()
        {
            var session = HttpContext.Session.GetString(SystemConstant.CartSession);
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if(session != null)
            {
                currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
            }
            return Ok(currentCart);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id, string languageId)
        {
            var product = await _productClient.GetByIdProductAsync(id, languageId);
            var session = HttpContext.Session.GetString(SystemConstant.CartSession);
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session != null)
            {
                currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
            }
            int quantity = 1;
            if (currentCart.Any(x => x.productId == id))
            {
                quantity = currentCart.First(x => x.productId == id).Quantity + 1;
            }
            var cartItem = new CartItemViewModel()
            {
                productId = id,
                Description=product.Description,
                Name = product.Name,
                Image = product.ProductslImage,
                Price =product.Price,
                Quantity = quantity,
            };
            currentCart.Add(cartItem);
            HttpContext.Session.SetString(SystemConstant.CartSession, JsonConvert.SerializeObject(currentCart));
            return Ok(currentCart);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCart(int id, int quantity)
        {
            var session = HttpContext.Session.GetString(SystemConstant.CartSession);
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session != null)
            {
                currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
            }
            foreach (var item in currentCart)
            {
                if (item.productId == id)
                {
                    if (quantity == 0)
                    {
                        currentCart.Remove(item);
                        break;
                    }
                    item.Quantity = quantity;
                }
            }
            HttpContext.Session.SetString(SystemConstant.CartSession, JsonConvert.SerializeObject(currentCart));
            return Ok(currentCart);
        }
        public IActionResult CheckOut()
        {
            return View(GetCheckOutViewModel());
        }
        [HttpPost]
        public IActionResult CheckOut(CheckOutViewModel checkOut)
        {
            var model = GetCheckOutViewModel();
            var orderDetails = new List<OrderDetailViewModel>();
            foreach (var item in model.CartItems)
            {
                orderDetails.Add(new OrderDetailViewModel()
                {
                    ProductId = item.productId,
                    Quantity = item.Quantity,
                });
            }
            var checkoutRequest = new CheckOutRequest()
            {
                Name = checkOut.CheckOutModel.Name,
                Address  = checkOut.CheckOutModel.Address,
                Email = checkOut.CheckOutModel.Email,
                Phone =checkOut.CheckOutModel.Phone,
                OrderDetailViews = orderDetails
            };
            TempData["SuccessMsg"] = "Order puschased successful";
            return View(model);
        }
        private CheckOutViewModel GetCheckOutViewModel()
        {
            var session = HttpContext.Session.GetString(SystemConstant.CartSession);
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session != null)
            {
                currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
            }
            var checkOutViewModel = new CheckOutViewModel()
            {
                CartItems = currentCart,
                CheckOutModel = new CheckOutRequest()
            };
            return checkOutViewModel;
        }
    }
}
