using WeatherForecastAPI.Model;

namespace WeatherForecastAPI.Interfaces
{
	public interface IWeatherService
	{
		public Task<CurrentWeather> GetCurrentWeatherAsync(string city);
	}
}
