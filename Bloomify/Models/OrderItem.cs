using System.ComponentModel.DataAnnotations;

namespace Bloomify.Models
{
    public class OrderItem
    {
        [Key] public int OrderItemID { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        public int orderID { get; set; }
        public Order? Orders { get; set; }

        public int productID { get; set; }
        public Product? Products { get; set; }
    }
}
