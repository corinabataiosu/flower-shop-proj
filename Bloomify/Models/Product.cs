using System.ComponentModel.DataAnnotations;

namespace Bloomify.Models
{
    public class Product
    {
        [Key] public int ProductID { get; set; }
        [Display(Name = "Product")]
        public String ProductName { get; set; }
        [Display(Name = "Description")]
        public String ProductDescription { get; set; }
        public float Price { get; set; }
        [Display(Name = "Image Path")]
        public String ImagePath { get; set; }
        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        public Category? Categories { get; set; }
        [Display(Name = "Provider")]
        public int ProviderID { get; set; }
        public Provider? Providers { get; set; }

        public ICollection<Review>? Reviews { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
