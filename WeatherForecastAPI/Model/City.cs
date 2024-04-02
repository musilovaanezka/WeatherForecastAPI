using System.Text.Json.Serialization;

namespace WeatherForecastAPI.Model
{
	public class City
	{
		[JsonPropertyName("id")]
		public long Id { get; set; }
		[JsonPropertyName("name")]
		public string? Name { get; set; }
		[JsonPropertyName("state")]
		public string? State { get; set; }
		[JsonPropertyName("country")]
		public string? Country { get; set; }
		[JsonPropertyName("coord")]
		public Coordinates Coord { get; set; }

		public City() { }
	}
}
