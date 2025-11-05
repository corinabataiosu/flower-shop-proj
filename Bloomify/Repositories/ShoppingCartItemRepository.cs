using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Data;

namespace Bloomify.Repositories
{
    public class ShoppingCartItemRepository : RepositoryBase<ShoppingCartItem>, IShoppingCartItemRepository
    {
        public ShoppingCartItemRepository(AppDbContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<ShoppingCartItem> GetItemsInCart(int cartId)
        {
            return _context.ShoppingCartItems
                .Where(item => item.shoppingCartID == cartId)
                .ToList();
        }

        public ShoppingCartItem GetItemByProductId(int productId, int cartId)
        {
            return _context.ShoppingCartItems
                           .FirstOrDefault(sci => sci.productID == productId && sci.shoppingCartID == cartId);
        }
        public void AddItem(ShoppingCartItem item)
        {
            _context.ShoppingCartItems.Add(item);
        }

        public void RemoveItem(ShoppingCartItem item)
        {
            _context.ShoppingCartItems.Remove(item);
        }
    }
}
