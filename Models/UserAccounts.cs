using System.ComponentModel.DataAnnotations;
namespace fbs.Models
{
    public class UserAccount
    {
        [Key]
        public int UserAccountId { get; set; }    // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        // Optional: You can add other attributes like Address, LoyaltyPoints, etc.
    }

}
