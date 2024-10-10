using System.ComponentModel.DataAnnotations;

namespace fbs.Models
{
    public class Plane
    {
        [Key]
        public int PlaneId { get; set; }    // Primary Key
        public string Airline  { get; set; }
        public int TotalSeats { get; set; }
    }
}
