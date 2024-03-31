using System.Text.Json;
using WeatherForecastAPI.Interfaces;
using WeatherForecastAPI.Model;

namespace WeatherForecastAPI.Services
{
	public class UserRepository : IUserRepository
	{
		private readonly string _filePath;

		public UserRepository(string filePath)
		{
			_filePath = filePath;
			if (!File.Exists(_filePath))
			{
				File.WriteAllText(_filePath, "[]");
			}
		}

		public void Add(User user)
		{
			var users = GetAllUsers();
			if (users.Any((u => u.Username == user.Username)))
			{
				throw new InvalidOperationException("Username already exists");
			}

			users.Add(user);
			File.WriteAllText(_filePath, JsonSerializer.Serialize(users));
		}

		public User GetUserByUsername(string username)
		{
			var users = GetAllUsers();
			return users.FirstOrDefault(u => u.Username == username);
		}

		public List<User> GetAllUsers()
		{
			var json = File.ReadAllText(_filePath);
			return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
		}
	}
}
