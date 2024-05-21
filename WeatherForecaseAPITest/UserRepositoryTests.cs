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
            if (File.Exists("users2.json")) File.Delete(filePath);
            var user = new User { Username = "testuser", Password = "testpassword" };
            var existingUsers = new List<User> { user };
            var usersJson = JsonSerializer.Serialize(existingUsers);
            File.WriteAllText(filePath, usersJson);
;
            var userRepository = new UserRepository(filePath);


            var result = userRepository.Add(user);

            Assert.Equal(null, result);
        }

        [Fact]
        public void GetUserByUsername_ExistingUser_ReturnsUser()
        {
            // Arrange
            var mockFile = new Mock<IFile>();
            var filePath = "users3.json"; // Sample file path
            if (File.Exists("users3.json")) File.Delete(filePath);
            var user = new User { Username = "testuser", Password = "testpassword" };
            var existingUsers = new List<User> { user };
            var usersJson = JsonSerializer.Serialize(existingUsers);
            File.WriteAllText(filePath, usersJson);

            mockFile.Setup(file => file.ReadAllText(filePath)).Returns(JsonSerializer.Serialize(existingUsers));

            var userRepository = new UserRepository(filePath);
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
            if (File.Exists("users4.json")) File.Delete(filePath);
            var user = new User { Username = "testuser", Password = "testpassword" };
            var existingUsers = new List<User> { user };
            var usersJson = JsonSerializer.Serialize(existingUsers);
            File.WriteAllText(filePath, usersJson);
            var username = "nonexistinguser";
            var userRepository = new UserRepository(filePath);

            mockFile.Setup(file => file.ReadAllText(filePath)).Returns(JsonSerializer.Serialize(existingUsers));

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
            var filePath = "users5.json"; // Sample file path
            if (File.Exists("users5.json")) File.Delete(filePath);
            var user = new User { Username = "testuser", Password = "testpassword" };
            var existingUsers = new List<User> { user };
            var usersJson = JsonSerializer.Serialize(existingUsers);
            File.WriteAllText(filePath, usersJson);
            var userRepository = new UserRepository(filePath);

            mockFile.Setup(file => file.ReadAllText(filePath)).Returns(JsonSerializer.Serialize(existingUsers));

            // Act
            var result = userRepository.GetAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingUsers.First().Username, result.First().Username);
        }

        [Fact]
        public void Add_Exception_ReturnsNull()
        {
            // Arrange
            var mockFile = new Mock<IFile>();
            var filePath = "users6.json"; // Sample file path
            if (File.Exists("users6.json")) File.Delete(filePath);
            var user = new User { Username = "testuser", Password = "testpassword" };
            var existingUsers = new List<User> { user };
            var usersJson = JsonSerializer.Serialize(existingUsers);
            File.WriteAllText(filePath, usersJson);
            var userRepository = new UserRepository(filePath);

            mockFile.Setup(file => file.ReadAllText(filePath)).Returns(JsonSerializer.Serialize(existingUsers));
            mockFile.Setup(file => file.WriteAllText(filePath, It.IsAny<string>()))
                .Throws(new IOException("Simulated exception"));

            // Act
            var result = userRepository.Add(user);

            // Assert
            Assert.Equal(result, null);
        }

        [Fact]
        public void GetAllUsers_ReturnsEmptyList_WhenJsonIsEmpty()
        {
            // Arrange
            var mockFile = new Mock<IFile>();
            var filePath = "users7.json"; // Sample file path
            if (File.Exists("users7.json")) File.Delete(filePath);
            File.WriteAllText(filePath, "[]");
            var userRepository = new UserRepository(filePath);

            mockFile.Setup(file => file.ReadAllText(filePath)).Returns("[]");

            // Act
            var result = userRepository.GetAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllUsers_ReturnsListOfUsers_WhenJsonIsNotEmpty()
        {
            // Arrange
            var mockFile = new Mock<IFile>();
            var filePath = "users8.json"; // Sample file path
            if (File.Exists("users8.json")) File.Delete(filePath);
            var user = new User { Username = "testuser", Password = "testpassword" };
            var existingUsers = new List<User> { user };
            var usersJson = JsonSerializer.Serialize(existingUsers);
            File.WriteAllText(filePath, usersJson);
            var userRepository = new UserRepository(filePath);

            mockFile.Setup(file => file.ReadAllText(filePath)).Returns(usersJson);

            // Act
            var result = userRepository.GetAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(user.Username, result.First().Username);
        }
    }
}
