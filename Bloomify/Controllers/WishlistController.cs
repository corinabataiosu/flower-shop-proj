using Bloomify.Controllers;
using Bloomify.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bloomify.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;
        private readonly IProductService _productService;

        public WishlistController(IWishlistService wishlistService, IProductService productService)
        {
            _wishlistService = wishlistService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var userId = GetCurrentUserId();
            var wishlist = _wishlistService.GetWishlistByUserId(userId);
            return View(wishlist);
        }

        [HttpPost]
        public IActionResult AddToWishlist(int productId)
        {
            var userId = GetCurrentUserId();
            var product = _productService.GetProductById(productId);

            if (product != null)
            {
                _wishlistService.AddToWishlist(product, userId);
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public IActionResult RemoveFromWishlist(int productId)
        {
            var userId = GetCurrentUserId();
            _wishlistService.RemoveFromWishlist(userId, productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult MoveToCart(int itemId)
        {
            var userId = GetCurrentUserId();
            _wishlistService.MoveToCart(userId, itemId);
            return RedirectToAction("Index");
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
