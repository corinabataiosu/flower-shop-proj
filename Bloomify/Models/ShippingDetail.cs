using System.ComponentModel.DataAnnotations;

namespace Bloomify.Models
{
    public class ShippingDetail
    {
        [Key] public int ShippingID { get; set; }
        public String Address { get; set; }
        public int PostalCode { get; set; }

        public int OrderID { get; set; }
        public Order? Orders { get; set; }
    }
}
