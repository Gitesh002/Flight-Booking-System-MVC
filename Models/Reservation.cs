using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fbs.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }    // Primary Key
        [ForeignKey("UserId")]
        public int UserId { get; set; }    // Foreign Key to UserAccount
        [ForeignKey("FlightId")]
        public int FlightId { get; set; }    // Foreign Key to Flight
        [ForeignKey("UserName")]
        public string UserName { get; set; } // Foreign Key to UserAccount
        public int NumberOfSeats { get; set; }
        public float TotalCost { get; set; }
        public DateTime ReservationDate { get; set; }

        //public Dictionary<string, string> Airports { get; set; } // Maps IATA code to city name
        public string DepartureCityCode { get; set; }
        public string ArrivalCityCode { get; set; }

        // Navigation properties for relationships
        public Flight Flight { get; set; }
        public UserAccount User { get; set; }
        public List<Passenger> Passengers { get; set; }
    }

}

