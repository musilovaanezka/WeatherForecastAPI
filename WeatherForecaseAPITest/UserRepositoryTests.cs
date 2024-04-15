using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherForecastAPI.Model;
using WeatherForecastAPI.Services;

namespace WeatherForecaseAPITest
{
    public class UserRepositoryTests
    {
        [Fact]
        public void Add_NewUser_ReturnsUser()
        {
            // Arrange
            var mockFile = new Mock<IFile>();
            var filePath = "users1.json"; // Sample file path
            if (File.Exists("users1.json")) File.Delete(filePath);
            var userRepository = new UserRepository(filePath);
            var user = new User { Username = "testuser", Password = "testpassword" };

            mockFile.Setup(file => file.ReadAllText(filePath)).Returns("[]");
            mockFile.Setup(file => file.WriteAllText(filePath, It.IsAny<string>()));

            // Act
            var result = userRepository.Add(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result);
        }

        [Fact]
        public void Add_ExistingUser_ReturnsNull_WhenUserExists()
        {
            var mockFile = new Mock<IFile>();
            var filePath = "users2.json"; // Sample file path
            mockFile.Setup(file => file.WriteAllText(filePath, It.IsAny<string>()));
            var userRepository = new UserRepository(filePath);
            var user = new User { Username = "testuser", Password = "testpassword" };
            var existingUsers = new List<User> { user };

            mockFile.Setup(file => file.ReadAllText(filePath)).Returns(JsonSerializer.Serialize(existingUsers));

            var result = userRepository.Add(user);

            Assert.Equal(result, null);
        }

        [Fact]
        public void GetUserByUsername_ExistingUser_ReturnsUser()
        {
            // Arrange
            var mockFile = new Mock<IFile>();
            var filePath = "users.json"; // Sample file path
            var userRepository = new UserRepository(filePath);
            var user = new User { Username = "testuser", Password = "testpassword" };
            var existingUsers = new List<User> { user };

            mockFile.Setup(file => file.ReadAllText(filePath)).Returns(JsonSerializer.Serialize(existingUsers));

            // Act
            var result = userRepository.GetUserByUsername("testuser");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Username, result.Username);
        }

        [Fact]
        public void GetUserByUsername_NonExistingUser_ReturnsNull()
        {
            // Arrange
            var mockFile = new Mock<IFile>();
            var filePath = "users4.json"; // Sample file path
            var userRepository = new UserRepository(filePath);
            var username = "nonexistinguser";

            mockFile.Setup(file => file.ReadAllText(filePath)).Returns("[]");

            // Act
            var result = userRepository.GetUserByUsername(username);

            // Assert
            Assert.Equal(result, null);
        }

        [Fact]
        public void GetAllUsers_ReturnsListOfUsers()
        {
            // Arrange
            var mockFile = new Mock<IFile>();
            var filePath = "users.json"; // Sample file path
            var userRepository = new UserRepository(filePath);
            var user = new List<User> { new User { Username = "testuser", Password = "testpassword" } };

            mockFile.Setup(file => file.ReadAllText(filePath)).Returns(JsonSerializer.Serialize(user));

            // Act
            var result = userRepository.GetAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.First().Username, result.First().Username);
        }

        [Fact]
        public void Add_Exception_ReturnsNull()
        {
            // Arrange
            var mockFile = new Mock<IFile>();
            var filePath = "users7.json"; // Sample file path
            var userRepository = new UserRepository(filePath);
            var user = new User { Username = "testuser", Password = "testpassword" };

            mockFile.Setup(file => file.ReadAllText(filePath)).Returns("[]");
            mockFile.Setup(file => file.WriteAllText(filePath, It.IsAny<string>()))
                .Throws(new IOException("Simulated exception"));

            // Act
            var result = userRepository.Add(user);

            // Assert
            Assert.Equal(result, null);
        }
    }
}
