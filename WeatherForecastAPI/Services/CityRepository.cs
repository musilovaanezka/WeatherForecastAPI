using WeatherForecastAPI.Interfaces;
using WeatherForecastAPI.Model;
using System.Text.Json;
using System.Diagnostics.Metrics;
using System.Xml.Linq;
namespace WeatherForecastAPI.Services
{
	public class CityRepository : ICityRepository
	{
		private readonly string _filePath;
		private List<City> _cityCasche;

		public CityRepository()
		{
			_filePath = Constants.citiesDataPath;
			_cityCasche = GetAllCities(_filePath);
		}
		public async Task<List<City>> Get(string? name, string? country, string? state)
		{
            return _cityCasche.Where(c =>
                (string.IsNullOrEmpty(name) || c.Name == name) &&
                (string.IsNullOrEmpty(country) || c.Country == country) &&
                (string.IsNullOrEmpty(state) || c.State == state)).ToList();
		}

        public async Task<City> GetById(long id)
        {
            return await Task.FromResult(_cityCasche.FirstOrDefault(c => c.Id == id));
        }

        public List<City> GetAllCities(string filepath)
		{
            try
            {
			    var json = File.ReadAllText(filepath);
			    return JsonSerializer.Deserialize<List<City>>(json);
            }
            catch (Exception ex)
            {
                return new List<City>();
            }
		}
	}
}
