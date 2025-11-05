using Bloomify.Models;

namespace Bloomify.Repositories.Interfaces
{
    public interface IShoppingCartRepository : IRepositoryBase<ShoppingCart>
    {
        ShoppingCart GetShoppingCartByUserID(int userID);
        IEnumerable<ShoppingCartItem> GetCartItemsByUserId(int userId);
        //void AddToCart(int userID, int productID, int quantity);
        void AddToCart(int userID, int productID);
        void CreateShoppingCart(ShoppingCart shoppingCart);
        void RemoveFromCart(int userID, int productID);
        void ClearCart(int userID);
        //void UpdateCartItem(int userID, int productID, int quantity);
        float GetTotalPrice(int userID);
    }
}
