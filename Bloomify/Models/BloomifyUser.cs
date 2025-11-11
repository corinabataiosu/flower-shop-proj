using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Bloomify.Models
{
    public class BloomifyUser : IdentityUser<int>
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string? ProfileImagePath { get; set; }
        public string? Address { get; set; }

        public ICollection<Order>? Orders { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
