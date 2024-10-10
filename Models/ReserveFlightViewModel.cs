namespace fbs.Models
{
    public class ReserveFlightViewModel
    {
        public int FlightId { get; set; }
        public int NumberOfSeats { get; set; }
        public List<Passenger> Passengers { get; set; }

        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepartureDate { get; set; }
        public float PricePerSeat { get; set; }

        public Dictionary<string, string> Airports { get; set; } // Maps IATA code to city name
    }
}
