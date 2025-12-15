using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Data;
using Microsoft.EntityFrameworkCore;

namespace Bloomify.Repositories
{
    public class WishlistRepository : RepositoryBase<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IQueryable<Wishlist> GetWishlistByUserId(int userId)
        {
            return FindByCondition(w => w.UserID == userId)
                   .Include(w => w.WishlistItems!)
                   .ThenInclude(i => i.Product);
        }
    }
}
