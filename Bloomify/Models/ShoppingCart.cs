using System.ComponentModel.DataAnnotations;

namespace Bloomify.Models
{
    public class ShoppingCart
    {
        [Key] public int ShoppingCartID { get; set; }
        public float TotalPrice { get; set; }

        public int userID { get; set; }
        public BloomifyUser? Users { get; set; }

        public ICollection<ShoppingCartItem>? ShoppingCartItems { get; set; }
    }
}
