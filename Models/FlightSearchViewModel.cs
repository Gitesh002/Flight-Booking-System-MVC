namespace fbs.Models
{
    public class FlightSearchViewModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public DateTime? Date { get; set; }

        
        // Search results
        public IEnumerable<Flight> Flights { get; set; }
        public Dictionary<string, string> Airports { get; set; } // Maps IATA code to city name
        public Dictionary<int, string> Airline { get; set; }
        public IEnumerable<Airport> ports { get; set; }
    }
}
