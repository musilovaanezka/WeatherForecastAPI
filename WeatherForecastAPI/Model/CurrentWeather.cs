using System.Text.Json.Serialization;

namespace WeatherForecastAPI.Model
{
	public class CurrentWeather
	{
		[JsonPropertyName("coord")]
		public Coordinates Coord { get; set; }
		[JsonPropertyName("weather")]
		public Weather[] Weather {  get; set; }
		[JsonPropertyName("base")]
		public string Base {  get; set; }
		[JsonPropertyName("main")]
		public MainWeather Main { get; set; }
		[JsonPropertyName("visibility")]
		public long Visibility { get; set; }
		[JsonPropertyName("wind")]
		public Wind Wind { get; set; }
		[JsonPropertyName("clouds")]
		public Clouds Clouds { get; set; }
		[JsonPropertyName("dt")]
		public long Dt { get; set; }
		[JsonPropertyName("sys")]
		public WeatherSys Sys { get; set; }
		[JsonPropertyName("timezone")]
		public long Timezone { get; set; }
		[JsonPropertyName("id")]
		public long Id { get; set; }
		[JsonPropertyName("name")]
		public string Name {  get; set; }
		[JsonPropertyName("cod")]
		public long Cod { get; set; }

		public CurrentWeather() { }
	}

	public class Clouds
	{
		[JsonPropertyName("all")]
		public long All {  get; set; }

		public Clouds() { }
	}

	public class MainWeather
	{
		[JsonPropertyName("temp")]
		public double Temp { get; set; }
		[JsonPropertyName("feels_like")]
		public double FeelsLike { get; set; }
		[JsonPropertyName("temp_min")]
		public double TempMin { get; set; }
		[JsonPropertyName("temp_max")]
		public double TempMax { get; set; }
		public long Pressure { get; set; }
		public long Humidity { get; set; }
		[JsonPropertyName("sea_level")]
		public long? SeaLevel { get; set; }
		[JsonPropertyName("grnd_level")]
		public long? GrndLevel { get; set; }

		public MainWeather() { }
	}

	public class WeatherSys
	{
		[JsonPropertyName("type")]
		public long Type { get; set; }
		[JsonPropertyName("id")]
		public long Id { get; set; }
		[JsonPropertyName("country")]
		public string Country { get; set; }
		[JsonPropertyName("sunrise")]
		public long Sunrise { get; set; }
		[JsonPropertyName("sunset")]
		public long Sunset { get; set; }

		public WeatherSys() { }
	}

	public class Weather
	{
		[JsonPropertyName("id")]
		public long Id { get; set; }
		[JsonPropertyName("main")]
		public string Main { get; set; }
		[JsonPropertyName("description")]
		public string Description { get; set; }
		[JsonPropertyName("icon")]
		public string Icon { get; set; }

		public Weather() { }
	}

	public class Wind
	{
		[JsonPropertyName("speed")]
		public double Speed { get; set; }
		[JsonPropertyName("deg")]
		public long Deg { get; set; }

		public Wind() { }
	}
}
