using Bloomify.Models;

namespace Bloomify.Repositories.Interfaces
{
    public interface IWishlistRepository : IRepositoryBase<Wishlist>
    {
        IQueryable<Wishlist> GetWishlistByUserId(int userId);
    }
}
