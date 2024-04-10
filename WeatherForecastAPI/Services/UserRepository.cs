using System.Text.Json;
using WeatherForecastAPI.Interfaces;
using WeatherForecastAPI.Model;

namespace WeatherForecastAPI.Services
{
	public class UserRepository : IUserRepository
	{
		private readonly string _filePath;

		public UserRepository()
		{
			_filePath = "../users.json";
			if (!File.Exists(_filePath))
			{
				File.WriteAllText(_filePath, "[]");
			}
		}

		public User Add(User user)
		{
			try
			{
				var users = GetAllUsers();
				if (users.Any((u => u.Username == user.Username)))
				{
					return null;
				}

				users.Add(user);
				File.WriteAllText(_filePath, JsonSerializer.Serialize(users));
				return user;
			}
			catch (Exception ex)
			{
				return null;
			}

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
