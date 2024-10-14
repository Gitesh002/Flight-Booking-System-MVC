using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fbs.Models
{
     public class Passenger
     {
        [Key]
        public int PassengerId { get; set; }

        [ForeignKey("ReservationId")]
        public int ReservationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
     }
}
