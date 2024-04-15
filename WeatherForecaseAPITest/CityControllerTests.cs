using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecastAPI.Controllers;
using WeatherForecastAPI.Interfaces;
using WeatherForecastAPI.Model;

namespace WeatherForecaseAPITest
{
    public class CityControllerTests
    {
        [Fact]
        public async Task GetCities_ValidRequest_ReturnsOk()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<CityController>>();
            var mockCityRepository = new Mock<ICityRepository>();
            var controller = new CityController(mockLogger.Object, mockCityRepository.Object);
            var expectedCities = new List<City> { new City { Name = "City1" }, new City { Name = "City2" } };
            mockCityRepository.Setup(repo => repo.Get(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expectedCities);

            // Act
            var result = await controller.GetCities("name", "country", "state") as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedCities, result.Value);
        }

        [Fact]
        public async Task GetById_ValiedId_ReturnsOk()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<CityController>>();
            var mockCityRepository = new Mock<ICityRepository>();
            var controller = new CityController(mockLogger.Object, mockCityRepository.Object);
            var expectedCity = new City { Id = 1, Name = "City1" };
            mockCityRepository.Setup(repo => repo.GetById(expectedCity.Id)).ReturnsAsync(expectedCity);

            // Act
            var result = await controller.GetById(expectedCity.Id) as OkObjectResult;

            // Assert 
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedCity, result.Value);
        }

        [Fact]
        public async Task GetById_invaliedId_ReturnsNotFound()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<CityController>>();
            var mockCityRepository = new Mock<ICityRepository>();
            var controller = new CityController(mockLogger.Object, mockCityRepository.Object);
            long id = 1;
            mockCityRepository.Setup(repo => repo.GetById(id)).ReturnsAsync((City)null);

            // Act
            var result = await controller.GetById(id) as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetCiies_notPublished_ReturnsNotFound() 
        {
            var mockLogger = new Mock<ILogger<CityController>>();
            var mockCityRepository = new Mock<ICityRepository>();
            mockCityRepository.Setup(repo => repo.Get(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Simulated exception"));
            var controller = new CityController(mockLogger.Object, mockCityRepository.Object);

            var result = await controller.GetCities("test", "test", "test") as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Simulated exception", result.Value);
        }

        [Fact]
        public async Task GetById_Exception_ReturnsBadRequest()
        {
            var mockLogger = new Mock<ILogger<CityController>>();
            var mockCityRepository = new Mock<ICityRepository>();
            mockCityRepository.Setup(repo => repo.GetById(1))
                 .ThrowsAsync(new Exception("Simulated exception"));
            var controller = new CityController(mockLogger.Object, mockCityRepository.Object);

            var result = await controller.GetById(1) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Simulated exception", result.Value);
        }
    }
}
