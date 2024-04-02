using Microsoft.AspNetCore.Mvc;
using WeatherForecastAPI.Interfaces;

namespace WeatherForecastAPI.Controllers
{
	[Route("api/weatherForecast")]
	[ApiController]
	public class WeatherForecastController : Controller
	{ 
		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IWeatherService _weatherService;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
		{
			_logger = logger;
			_weatherService = weatherService;
		}

		[HttpGet]
		public async Task<IActionResult> GetWeather(string city)
		{
			try
			{
				var weatherData = await _weatherService.GetCurrentWeatherAsync(city);
				return Ok(weatherData);
			} 
			catch (Exception ex)
			{
				_logger.LogError($"{ex}");
				return BadRequest(ex.Message);
			}
		}
	}
}
