using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fbs.Models
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }    // Primary Key
        [ForeignKey("PlaneId")]
        public int PlaneId { get; set; }
        public string DepartureCity { get; set; }   // Airport IATA
        public string ArrivalCity { get; set; }     // Airport IATA 
        public DateTime DepartureDate { get; set; }
        public float Price { get; set; }
        public int AvailableSeats { get; set; }
        // Additional optional properties (if needed): Duration, Airline, etc.
    }

}
