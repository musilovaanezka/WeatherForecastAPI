using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecastAPI;
using WeatherForecastAPI.Interfaces;
using WeatherForecastAPI.Services;

namespace WeatherForecaseAPITest
{
    public class ServiceExtensionsTests
    {
        [Fact]
        public void AddRepositories_AddsRepositoriesCorrectly()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddRepositories();

            // Assert
            var provider = services.BuildServiceProvider();
            var userRepository = provider.GetService<IUserRepository>();
            var cityRepository = provider.GetService<ICityRepository>();

            Assert.NotNull(userRepository);
            Assert.IsType<UserRepository>(userRepository);

            Assert.NotNull(cityRepository);
            Assert.IsType<CityRepository>(cityRepository);
        }
    }
}
