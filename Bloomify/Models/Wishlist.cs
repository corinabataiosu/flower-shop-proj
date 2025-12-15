using System.ComponentModel.DataAnnotations;

namespace Bloomify.Models
{
    public class Wishlist
    {
        [Key]
        public int WishlistID { get; set; }

        public int UserID { get; set; }
        public BloomifyUser? User { get; set; }

        public ICollection<WishlistItem>? WishlistItems { get; set; }
    }
}
