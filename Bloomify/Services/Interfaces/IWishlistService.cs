using Bloomify.Models;

namespace Bloomify.Services.Interfaces
{
    public interface IWishlistService
    {
        Wishlist GetWishlistByUserId(int userId);
        void CreateWishlist(Wishlist wishlist);
        void AddToWishlist(Product product, int userId);
        void RemoveFromWishlist(int userId, int productId);
        void RemoveItem(WishlistItem item);
        void MoveToCart(int userId, int itemId);
    }
}
