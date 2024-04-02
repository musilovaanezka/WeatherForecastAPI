using WeatherForecastAPI.Interfaces;
using WeatherForecastAPI.Model;
using System.Text.Json;
namespace WeatherForecastAPI.Services
{
	public class CityRepository : ICityRepository
	{
		private readonly string _filePath;

		public CityRepository()
		{
			string dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
			_filePath = Path.Combine(dataFolderPath, "cities.json");
		}
		public async Task<List<City>> Get(string? name, string? country, string? state)
		{
			//var cityList = GetAllCities();
			//return cityList.Where(c => 
			//	(string.IsNullOrEmpty(name) || c.Name == name) &&
			//	(string.IsNullOrEmpty(country) || c.Country == country) &&
			//	(string.IsNullOrEmpty(state) || c.State == state)
			//).ToList();

			using (FileStream fs = File.OpenRead(_filePath))
			{
				List<City> list = new List<City>();
				using (JsonDocument doc = await JsonDocument.ParseAsync(fs))
				{
					foreach(JsonElement el in doc.RootElement.EnumerateArray())
					{ 
						try
						{
							City c = JsonSerializer.Deserialize<City>(el);

							if ((string.IsNullOrEmpty(name) || c.Name == name) &&
								(string.IsNullOrEmpty(country) || c.Country == country) &&
								(string.IsNullOrEmpty(state) || c.State == state))
							{
								list.Add(c);
							}
						}
						catch (JsonException ex)
						{
							Console.WriteLine($"Failed to parse JSON element: {ex.Message}");
						}
					}
				}
				return list;
			}
		}

		private List<City> GetAllCities()
		{
			var json = File.ReadAllText(_filePath);
			return JsonSerializer.Deserialize<List<City>>(json);
		}
	}
}
