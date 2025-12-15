using Bloomify.Models;

namespace Bloomify.Repositories.Interfaces
{
    public interface IWishlistItemRepository : IRepositoryBase<WishlistItem>
    {
        WishlistItem GetItemByProductId(int productId, int wishlistId);
    }
}
