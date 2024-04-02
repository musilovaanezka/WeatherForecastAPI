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
				_userRepository.Add(user);
			} 
			catch (Exception ex)
			{
				_logger.LogError($"{ex}");
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
					return Ok("Login successful");
				} 
				else if (userByUsername == null)
				{
					return Unauthorized("Username does not exist");
				} 
				else
				{
					return Unauthorized("Wrong password");
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
