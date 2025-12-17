using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bloomify.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IShoppingCartService _shoppingCartService;

        public WishlistService(IRepositoryWrapper repositoryWrapper, IShoppingCartService shoppingCartService)
        {
            _repositoryWrapper = repositoryWrapper;
            _shoppingCartService = shoppingCartService;
        }

        public Wishlist GetWishlistByUserId(int userId)
        {
            return _repositoryWrapper.WishlistRepository
                .GetWishlistByUserId(userId)
                .FirstOrDefault();
        }

        public void CreateWishlist(Wishlist wishlist)
        {
            _repositoryWrapper.WishlistRepository.Create(wishlist);
            _repositoryWrapper.Save();
        }

        public void AddToWishlist(Product product, int userId)
        {
            var wishlist = GetWishlistByUserId(userId);
            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    UserID = userId,
                    WishlistItems = new List<WishlistItem>()
                };
                CreateWishlist(wishlist);
            }

            var existingItem = _repositoryWrapper.WishlistItemRepository
                .GetItemByProductId(product.ProductID, wishlist.WishlistID);

            if (existingItem == null)
            {
                var newItem = new WishlistItem
                {
                    ProductID = product.ProductID,
                    WishlistID = wishlist.WishlistID
                };
                _repositoryWrapper.WishlistItemRepository.Create(newItem);
                _repositoryWrapper.Save();
            }
        }

        public void RemoveFromWishlist(int userId, int productId)
        {
            var wishlist = GetWishlistByUserId(userId);
            if (wishlist == null) return;

            var item = _repositoryWrapper.WishlistItemRepository
                .GetItemByProductId(productId, wishlist.WishlistID);

            if (item != null)
            {
                _repositoryWrapper.WishlistItemRepository.Delete(item);
                _repositoryWrapper.Save();
            }
        }

        public void RemoveItem(WishlistItem item)
        {
            if (item != null)
            {
                _repositoryWrapper.WishlistItemRepository.Delete(item);
                _repositoryWrapper.Save();
            }
        }

        public void MoveToCart(int userId, int itemId)
        {
            var item = _repositoryWrapper.WishlistItemRepository
                .FindByCondition(i => i.WishlistItemID == itemId)
                .AsTracking()
                .Include(i => i.Product)
                .FirstOrDefault();

            if (item != null)
            {
                if (item.Product != null)
                {
                    _shoppingCartService.AddToCart(item.Product, 1, userId);
                }
                RemoveItem(item);
            }
        }
    }
}
