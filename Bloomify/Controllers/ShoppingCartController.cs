using Microsoft.AspNetCore.Mvc;
using Bloomify.Models;
using Bloomify.Data;
using Bloomify.Services;
using Bloomify.Services.Interfaces;
using System.Security.Claims;
using Bloomify.Repositories;
using Bloomify.Repositories.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Bloomify.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly UserManager<BloomifyUser> _userManager;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IProductService productService, IRepositoryWrapper repositoryWrapper, IUserService userService, IOrderService orderService, UserManager<BloomifyUser> userManager)
        {
            _shoppingCartService = shoppingCartService;
            _productService = productService;
            _userService = userService;
            _orderService = orderService;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userId = 1;
            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
                return RedirectToAction("Login", "Account");

            var cart = _shoppingCartService.GetShoppingCartByUserID(userId);
            if (cart == null)
                return View(new List<ShoppingCartItem>());

            var items = cart.ShoppingCartItems?.ToList() ?? new List<ShoppingCartItem>();
            return View(items);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            var userId = 1;
            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
                return RedirectToAction("Login", "Account"); // fallback în caz de anonimi

            var cart = _shoppingCartService.GetShoppingCartByUserID(userId);
            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    userID = userId,
                };
                _shoppingCartService.CreateShoppingCart(cart);
            }

            var product = _productService.GetProductById(productId);
            if (product != null)
            {
                _shoppingCartService.AddToCart(product, quantity, userId);
            }

            return Redirect(Request.Headers["Referer"].ToString());

            //return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public IActionResult RemoveFromCart(int itemId)
        {
            var cart = _shoppingCartService.GetShoppingCartByUserID(1);
            //var cart = _shoppingCartService.GetShoppingCartByUserID(
              //  int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0"));

            var item = cart?.ShoppingCartItems?.FirstOrDefault(i => i.ShoppingCartItemID == itemId);
            if (item != null)
            {
                _shoppingCartService.RemoveItemFromCart(item);
            }

            return Redirect(Request.Headers["Referer"].ToString());

            //return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public IActionResult ClearCart()
        {
            var userId = 1;
            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            _shoppingCartService.ClearShoppingCart(userId);
            return Redirect(Request.Headers["Referer"].ToString());
            //return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public IActionResult UpdateQuantity(int itemId, string action)
        {
            var userId = 1;
            var cart = _shoppingCartService.GetShoppingCartByUserID(userId);
            var item = cart?.ShoppingCartItems?.FirstOrDefault(i => i.ShoppingCartItemID == itemId);

            if (item != null)
            {
                if (action == "increase")
                    item.Quantity += 1;
                else if (action == "decrease" && item.Quantity > 1)
                    item.Quantity -= 1;

                _shoppingCartService.Update(item);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Checkout()
        {
            var user = _userService.GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cartItems = _shoppingCartService.GetCartItems();
            if (cartItems == null || !cartItems.Any())
            {
                TempData["Message"] = "Your cart is empty.";
                return RedirectToAction("Index");
            }

            var shippingDetail = new ShippingDetail
            {
                Address = user.Address,
                PostalCode = 108910
            };

            ViewBag.CartItems = cartItems;
            return View(shippingDetail);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Checkout(ShippingDetail shippingDetail)
        {
            var user = _userService.GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cartItems = _shoppingCartService.GetCartItems(); 
            if (cartItems == null || !cartItems.Any())
            {
                TempData["Message"] = "Your cart is empty.";
                return RedirectToAction("Index");
            }

            var order = new Order
            {
                userID = user.Id,
                Status = "Pending",
                TotalPrice = cartItems.Sum(item => item.Products.Price * item.Quantity),
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    productID = item.Products.ProductID,
                    Quantity = item.Quantity,
                    Price = item.Products.Price
                }).ToList(),
                ShippingDetails = new List<ShippingDetail> { shippingDetail }
            };

            _orderService.CreateOrder(order); 

            _shoppingCartService.ClearShoppingCart(1);

            return RedirectToAction("OrderConfirmation", new { id = order.OrderID });
        }

        [Authorize]
        public IActionResult OrderConfirmation(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        private int GetCurrentUserId()
        {
            if (User?.Identity?.IsAuthenticated != true)
                return 0;

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return 0;

            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }
    }
}
