using WeatherForecastAPI.Model;
namespace WeatherForecastAPI.Interfaces
{
	public interface IUserRepository
	{
		public void Add(User user);

		public User GetUserByUsername(string username);

		public List<User> GetAllUsers();
	}
}
