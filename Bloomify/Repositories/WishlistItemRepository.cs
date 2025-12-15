using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Data;

namespace Bloomify.Repositories
{
    public class WishlistItemRepository : RepositoryBase<WishlistItem>, IWishlistItemRepository
    {
        public WishlistItemRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public WishlistItem GetItemByProductId(int productId, int wishlistId)
        {
            return FindByCondition(i => i.ProductID == productId && i.WishlistID == wishlistId).FirstOrDefault();
        }
    }
}
