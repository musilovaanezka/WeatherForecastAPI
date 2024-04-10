using WeatherForecastAPI.Model;

namespace WeatherForecastAPI.Interfaces
{
	public interface ICityRepository
	{
		public Task<List<City>> Get(string? name, string? country, string? state);

		public Task<City> GetById(long id);
	}
}
