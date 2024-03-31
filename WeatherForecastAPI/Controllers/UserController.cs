using Microsoft.AspNetCore.Mvc;

namespace WeatherForecastAPI.Controllers
{
	[Route("api/users")]
	[ApiController]
	public class UserController : Controller
	{
		private readonly ILogger<UserController> _logger;
		
	}
}
