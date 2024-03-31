using WeatherForecastAPI.Interfaces;

namespace WeatherForecastAPI
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			return services.AddSingleton<IUserRepository, IUserRepository>();
		}
	}
}
