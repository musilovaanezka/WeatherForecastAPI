using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecastAPI.Model;
using WeatherForecastAPI.Services;
using System.Text.Json;
using System.IO;

namespace WeatherForecaseAPITest
{
    public class CityRepositoryTests
    {
        [Fact]
        public async Task Get_ReturnsFilteredCities()
        {
            var cityRepository = new CityRepository();
            var cities = new List<City>
            {
                new City { Id = 3551608, Name = "La Salud", Country = "CU", State = "", Coord = new Coordinates { Lat = 22.871389, Lon = -82.423889 } },
                new City { Id = 3552028, Name = "La Playa", Country = "CU", State = "", Coord = new Coordinates { Lat = 20.346939, Lon = -74.504723 } }
            };
            var filteredCities = cities.Where(c => c.Name == "La Salud").ToList();

            // Act
            var result = await cityRepository.Get("La Salud", "CU", null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(filteredCities.First().Id, result.First().Id);
        }

        [Fact]
        public async Task GetById_ReturnsCity()
        {
            var cityRepository = new CityRepository();
            var cities = new List<City>
            {
                new City { Id = 3551608, Name = "La Salud", Country = "CU", State = "", Coord = new Coordinates { Lat = 22.871389, Lon = -82.423889 } },
                new City { Id = 3552028, Name = "La Playa", Country = "CU", State = "", Coord = new Coordinates { Lat = 20.346939, Lon = -74.504723 } }
            };
            var cityId = 3551608;
            var expectedCity = cities.FirstOrDefault(c => c.Id == cityId);


            var result = await cityRepository.GetById(cityId);

            Assert.NotNull(result);
            Assert.Equal(expectedCity.Id, result.Id);
        }

        [Fact]
        public void GetAllCities_Exception_ReturnsEmptyList()
        {
            var mockFile = new Mock<IFile>();
            var filePath = "cities.json"; // Sample file path
            var cityRepository = new CityRepository();
            mockFile.Setup(file => file.ReadAllText(filePath)).Throws(new IOException("Simulated exception"));

            var result = cityRepository.GetAllCities(filePath); 

            Assert.NotNull(result);
            Assert.Empty(result);
        }
        [Fact]
        public async Task Get_ReturnsAllCities_WhenNoFiltersAreProvided()
        {
            // Arrange
            var cityRepository = new CityRepository();

            // Act
            var result = await cityRepository.Get(null, null, null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(209579, result.Count);
        }

        [Fact]
        public async Task Get_ReturnsCitiesByCountry_WhenOnlyCountryIsProvided()
        {
            // Arrange
            var cityRepository = new CityRepository();

            // Act
            var result = await cityRepository.Get(null, "CU", null);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, city => Assert.Equal("CU", city.Country));
        }

        [Fact]
        public async Task Get_ReturnsEmptyList_WhenNoCitiesMatchFilters()
        {
            // Arrange
            var cityRepository = new CityRepository();

            // Act
            var result = await cityRepository.Get("Nonexistent City", null, null);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Get_ReturnsCitiesByName_WhenOnlyNameIsProvided()
        {
            // Arrange
            var cityRepository = new CityRepository();

            // Act
            var result = await cityRepository.Get("La Salud", null, null);

            // Assert
            Assert.NotNull(result);
            Assert.All(result, city => Assert.Equal("La Salud", city.Name));
        }

        [Fact]
        public async Task Get_ReturnsCitiesByState_WhenOnlyStateIsProvided()
        {
            // Arrange
            var cityRepository = new CityRepository();

            // Act
            var result = await cityRepository.Get(null, null, "SomeState");

            // Assert
            Assert.NotNull(result);
            Assert.All(result, city => Assert.Equal("SomeState", city.State));
        }

        [Fact]
        public async Task Get_ReturnsCitiesByCountryAndState_WhenCountryAndStateAreProvided()
        {
            // Arrange
            var cityRepository = new CityRepository();

            // Act
            var result = await cityRepository.Get(null, "CU", "SomeState");

            // Assert
            Assert.NotNull(result);
            Assert.All(result, city => Assert.Equal("CU", city.Country));
        }

        [Fact]
        public async Task Get_ReturnsCitiesByLat_WhenLatIsProvided()
        {
            // Arrange
            var cityRepository = new CityRepository();
            var targetLat = 22.871389;

            // Act
            var result = await cityRepository.Get(null, null, null);

            // Assert
            Assert.NotNull(result);
            var cityWithTargetLat = result.FirstOrDefault(city => city.Coord.Lat == targetLat);
            Assert.NotNull(cityWithTargetLat);
        }

        [Fact]
        public async Task Get_ReturnsCitiesByLon_WhenLonIsProvided()
        {
            // Arrange
            var cityRepository = new CityRepository();
            var targetLon = -82.423889;

            // Act
            var result = await cityRepository.Get(null, null, null);

            // Assert
            Assert.NotNull(result);
            var cityWithTargetLon = result.FirstOrDefault(city => city.Coord.Lon == targetLon);
            Assert.NotNull(cityWithTargetLon);
        }
    }

    public interface IFile
    {
        string ReadAllText(string path);
        void WriteAllText(string path, string contents);
    }
}
