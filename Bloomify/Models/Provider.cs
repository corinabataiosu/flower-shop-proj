using System.ComponentModel.DataAnnotations;

namespace Bloomify.Models
{
    public class Provider
    {
        [Key] public int ProviderID { get; set; }
        public String ProviderName { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
