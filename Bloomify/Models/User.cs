using System.ComponentModel.DataAnnotations;

namespace Bloomify.Models
{
    public class User
    {
        [Key] public int UserID { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String PhoneNumber { get; set; }
        //public String BirthDate { get; set; }
        public String Address { get; set; }

        public ICollection<Order>? Orders { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
