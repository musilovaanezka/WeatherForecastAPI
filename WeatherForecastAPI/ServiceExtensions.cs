using WeatherForecastAPI.Interfaces;
using WeatherForecastAPI.Services;

namespace WeatherForecastAPI
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			return services
				.AddSingleton<IUserRepository, UserRepository>()
				.AddSingleton<IWeatherService, WeatherService>()
				.AddSingleton<ICityRepository, CityRepository>();
		}
	}
}
