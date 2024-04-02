using Microsoft.AspNetCore.Mvc;
using WeatherForecastAPI.Interfaces;
using WeatherForecastAPI.Model;

namespace WeatherForecastAPI.Controllers
{
	[Route("api/cities")]
	[ApiController]
	public class CityController : Controller
	{
		private readonly ILogger<CityController> _logger;
		private readonly ICityRepository _cityRepository;

		public CityController(ILogger<CityController> logger, ICityRepository cityRepository)
		{
			_logger = logger;
			_cityRepository = cityRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetCities(string? name, string? country, string? state)
		{
			try
			{
				var cities = await _cityRepository.Get(name, country, state);
				return Ok(cities);
			} 
			catch (Exception ex)
			{
				_logger.LogError($"{ex}");
				return BadRequest(ex.Message);
			}
		}
	}
}
