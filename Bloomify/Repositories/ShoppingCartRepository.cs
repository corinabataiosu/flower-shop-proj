using Bloomify.Models;
using Bloomify.Repositories.Interfaces;
using Bloomify.Data;
using Microsoft.EntityFrameworkCore;

namespace Bloomify.Repositories
{
    public class ShoppingCartRepository : RepositoryBase<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(AppDbContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public ShoppingCart GetShoppingCartByUserID(int userID)
        {
            return _context.ShoppingCarts
                .Include(c => c.ShoppingCartItems)
                .ThenInclude(i => i.Products)
                .FirstOrDefault(c => c.userID == userID);
        }
        public void CreateShoppingCart(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
            _context.SaveChanges();
        }

        public void AddToCart(int userID, int productID)
        {
            var shoppingCart = GetShoppingCartByUserID(userID);
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    userID = userID,
                    TotalPrice = 0
                };
                CreateShoppingCart(shoppingCart);
            }

            var cartItem = _context.ShoppingCartItems
                .FirstOrDefault(sci => sci.shoppingCartID == shoppingCart.ShoppingCartID && sci.productID == productID);

            if (cartItem == null)
            {
                cartItem = new ShoppingCartItem
                {
                    shoppingCartID = shoppingCart.ShoppingCartID,
                    productID = productID,
                    Quantity = 1
                };
                _context.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }

            _context.SaveChanges();
        }

        public void RemoveFromCart(int userID, int productID)
        {
            var shoppingCart = GetShoppingCartByUserID(userID);
            if (shoppingCart == null)
            {
                throw new Exception("Shopping cart not found");
            }

            var cartItem = _context.ShoppingCartItems
                .FirstOrDefault(sci => sci.shoppingCartID == shoppingCart.ShoppingCartID && sci.productID == productID);

            if (cartItem != null)
            {
                _context.ShoppingCartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        }

        public void ClearCart(int userID)
        {
            var shoppingCart = GetShoppingCartByUserID(userID);
            if (shoppingCart != null)
            {
                var cartItems = _context.ShoppingCartItems
                    .Where(sci => sci.shoppingCartID == shoppingCart.ShoppingCartID)
                    .ToList();

                _context.ShoppingCartItems.RemoveRange(cartItems);
                shoppingCart.TotalPrice = 0;
                _context.SaveChanges();
            }
        }

        public float GetTotalPrice(int userID)
        {
            var shoppingCart = GetShoppingCartByUserID(userID);
            if (shoppingCart == null)
            {
                return 0;
            }
           
            return shoppingCart.ShoppingCartItems.Sum(item => item.Price * item.Quantity); ;
        }

        public IEnumerable<ShoppingCartItem> GetCartItemsByUserId(int userId)
        {
            var cart = GetShoppingCartByUserID(userId);
            return cart?.ShoppingCartItems ?? new List<ShoppingCartItem>();
        }
    }
}
