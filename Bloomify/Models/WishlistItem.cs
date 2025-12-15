using System.ComponentModel.DataAnnotations;

namespace Bloomify.Models
{
    public class WishlistItem
    {
        [Key]
        public int WishlistItemID { get; set; }

        public int WishlistID { get; set; }
        public Wishlist? Wishlist { get; set; }

        public int ProductID { get; set; }
        public Product? Product { get; set; }
    }
}
