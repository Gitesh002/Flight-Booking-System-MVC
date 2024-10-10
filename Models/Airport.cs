using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace fbs.Models
{
    public class Airport
    {
        [Key]
        [JsonPropertyName("IATA_code")]
        public string IATA_code { get; set; }

        [JsonPropertyName("ICAO_code")]
        public string ICAO_code { get; set; }

        [JsonPropertyName("airport_name")]
        public string airport_name { get; set; }

        [JsonPropertyName("city_name")]
        public string city_name { get; set; }
    }
}
