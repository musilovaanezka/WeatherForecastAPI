using System.Text.Json.Serialization;

namespace WeatherForecastAPI.Model
{
	public class CityGeolocation
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("local_names")]
		public Dictionary<string, string> LocalNames { get; set; }
		[JsonPropertyName("lat")]
		public double Lat { get; set; }
		[JsonPropertyName("lon")]
		public double Lon { get; set; }
		[JsonPropertyName("country")]
		public string Country { get; set; }
		[JsonPropertyName("state")]
		public string State { get; set; }

		public CityGeolocation() { }
	}
}
