using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WeatherForecastAPI.Controllers;
using WeatherForecastAPI.Interfaces;
using WeatherForecastAPI.Model;

namespace WeatherForecaseAPITest
{
    public class UserControllerTests
    {
        [Fact]
        public void SignUp_ValidUser_ReturnsOk()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UserController>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var controller = new UserController(mockLogger.Object, mockUserRepository.Object);
            var user = new User { Username = "testuser", Password = "testpassword" };

            mockUserRepository.Setup(repo => repo.Add(It.IsAny<User>())).Returns(user);

            // Act
            var result = controller.SignUp(user) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(user, result.Value);
        }

        [Fact]
        public void SignUp_InvalidUser_ReturnsBadRequest()
        {
            var mockLogger = new Mock<ILogger<UserController>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var controller = new UserController(mockLogger.Object, mockUserRepository.Object);
            var user = new User();

            mockUserRepository.Setup(repo => repo.Add(It.IsAny<User>())).Returns((User)null);

            var result = controller.SignUp(user) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("An error occurred while processing your request", result.Value);
        }

        [Fact]
        public void SignIn_ValidUser_ReturnsOk()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UserController>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var controller = new UserController(mockLogger.Object, mockUserRepository.Object);
            var user = new User { Username = "testuser", Password = "testpassword" };

            mockUserRepository.Setup(repo => repo.GetUserByUsername(user.Username)).Returns(user);

            // Act
            var result = controller.SignIn(user) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(user, result.Value);
        }

        [Fact]
        public void SignIn_InvalidUser_ReturnsUnauthorized()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UserController>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var controller = new UserController(mockLogger.Object, mockUserRepository.Object);
            var user = new User { Username = "testuser", Password = "testpassword" };

            mockUserRepository.Setup(repo => repo.GetUserByUsername(user.Username)).Returns((User)null);

            // Act
            var result = controller.SignIn(user) as StatusCodeResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }

        [Fact]
        public void SignUp_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UserController>>();
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.Add(It.IsAny<User>())).Throws(new Exception());
            var controller = new UserController(mockLogger.Object, mockUserRepository.Object);
            var user = new User { Username = "testuser", Password = "testpassword" };

            // Act
            var result = controller.SignUp(user) as StatusCodeResult;

            // Assert
            Assert.Equal(result, null);
        }

        [Fact]
        public void SignIn_NullUser_ReturnsBadRequest()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UserController>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var controller = new UserController(mockLogger.Object, mockUserRepository.Object);

            // Act
            var result = controller.SignIn(null) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
    }
}