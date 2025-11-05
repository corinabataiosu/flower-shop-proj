using System.ComponentModel.DataAnnotations;

namespace Bloomify.Models
{
    public class Order
    {
        [Key] public int OrderID { get; set; }
        public float TotalPrice { get; set; }
        public String Status { get; set; }

        public int userID { get; set; }
        public User? Users { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<ShippingDetail>? ShippingDetails { get; set; }
    }
}
