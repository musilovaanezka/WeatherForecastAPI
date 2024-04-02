using System.ComponentModel;
using System.Text.Json;
using WeatherForecastAPI.Interfaces;
using WeatherForecastAPI.Model;
using static System.Net.WebRequestMethods;

namespace WeatherForecastAPI.Services
{
	public class WeatherService : IWeatherService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ICityRepository _cityRepository;
		private readonly string _apiKey = "27f5e232e3b11725bc87ed47808bead0";

		public WeatherService(IHttpClientFactory httpClientFactory, ICityRepository cityRepository)
		{
			_httpClientFactory = httpClientFactory;
			_cityRepository = cityRepository;
		}

		public async Task<CurrentWeather> GetCurrentWeatherAsync(string city)
		{
			var httpClient = _httpClientFactory.CreateClient();

			var geolocation = await _cityRepository.Get(city, null, null);

			string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?appid={_apiKey}&lat={geolocation[0].Coord.Lat}&lon={geolocation[0].Coord.Lon}&units=metric";
			var response = await httpClient.GetAsync(apiUrl);
			var weather = await response.Content.ReadAsStringAsync();
			CurrentWeather currentWeathers = JsonSerializer.Deserialize<CurrentWeather>(weather);

			return currentWeathers;
		}

		//private async Task<CityGeolocation> getGeolocationOfCityAsync(string city)
		//{
		//	string geoURL = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&appid={_apiKey}";

		//	var httpClient = _httpClientFactory.CreateClient();
		//	var goeResponse = await httpClient.GetAsync(geoURL);
		//	var geo = await goeResponse.Content.ReadAsStringAsync();
		//	List<CityGeolocation> geolocations = JsonSerializer.Deserialize<List<CityGeolocation>>(geo);

		//	return geolocations[0];
		//}
	}
}
