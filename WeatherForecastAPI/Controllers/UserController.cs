using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WeatherForecastAPI.Model;
using WeatherForecastAPI.Interfaces;

namespace WeatherForecastAPI.Controllers
{
	[Route("api/users")]
	[ApiController]
	public class UserController : Controller
	{
		private readonly ILogger<UserController> _logger;
		private readonly IUserRepository _userRepository;

		public UserController(ILogger<UserController> logger, IUserRepository userRepository)
		{
			_logger = logger;
			_userRepository = userRepository;
		}

		[HttpPost("signup")]
		public IActionResult SignUp([FromBody] User user)
		{
			try
			{
				User response = _userRepository.Add(user);
				if (response == null)
				{
                    return StatusCode(400, "An error occurred while processing your request");
                }
				return Ok(user);
			} 
			catch (Exception ex)
			{
				_logger.LogError($"{ex}");
                return StatusCode(500, "An error occurred while processing your request");
            }
			return Ok();
		}

		[HttpPost("signin")]
		public IActionResult SignIn([FromBody] User user)
		{
			try
			{
				var userByUsername = _userRepository.GetUserByUsername(user.Username);
				if (userByUsername != null && userByUsername.Password == user.Password)
				{
					return Ok(user);
				} 
				else
				{
					return Unauthorized();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex}");
			}
			return BadRequest();
		}
	}
}
