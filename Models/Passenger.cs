using System.ComponentModel.DataAnnotations;

namespace fbs.Models
{
     public class Passenger
     {
        [Key]
        public int PassengerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
     }
}
