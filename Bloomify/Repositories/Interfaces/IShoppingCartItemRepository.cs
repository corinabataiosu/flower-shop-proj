using Bloomify.Models;

namespace Bloomify.Repositories.Interfaces
{
    public interface IShoppingCartItemRepository : IRepositoryBase<ShoppingCartItem>
    {
        //void AddItemToCart(int cartId, int productId, int quantity);
        //void RemoveItemFromCart(int cartId, int productId);
        //void UpdateItemQuantity(int cartId, int productId, int quantity);
        IEnumerable<ShoppingCartItem> GetItemsInCart(int cartId);
        ShoppingCartItem GetItemByProductId(int cartId, int productId);
        void AddItem(ShoppingCartItem item);
        void RemoveItem(ShoppingCartItem item);
    }
}
