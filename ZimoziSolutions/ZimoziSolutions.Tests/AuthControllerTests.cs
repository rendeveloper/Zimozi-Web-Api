using Microsoft.AspNetCore.Mvc;
using Moq;
using ZimoziSolutions.ApiModels.Tokens;
using ZimoziSolutions.ApiModels.Users;
using ZimoziSolutions.Controllers;
using ZimoziSolutions.Core.Interfaces.Users;
using ZimoziSolutions.Domain.Users;

namespace ZimoziSolutions.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthCoreService> _authCoreServiceMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _authCoreServiceMock = new Mock<IAuthCoreService>();
            _controller = new AuthController(_authCoreServiceMock.Object);
        }

        [Fact]
        public async Task Register_ReturnsOk_WithUser()
        {
            // Arrange
            var userModel = new UserModel { Username = "testuser", Password = "password" };
            var user = new User { Id = 1, Username = "testuser", Role = "User" };
            _authCoreServiceMock.Setup(s => s.RegisterAsync(userModel)).ReturnsAsync(user);

            // Act
            var result = await _controller.Register(userModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal("testuser", returnedUser.Username);
        }

        [Fact]
        public async Task Login_ReturnsOk_WithTokenResponse()
        {
            // Arrange
            var userModel = new UserModel { Username = "testuser", Password = "password" };
            var tokenResponse = new TokenResponseModel
            {
                AccessToken = "access-token",
                RefreshToken = "refresh-token"
            };
            _authCoreServiceMock.Setup(s => s.LoginAsync(userModel)).ReturnsAsync(tokenResponse);

            // Act
            var result = await _controller.Login(userModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedToken = Assert.IsType<TokenResponseModel>(okResult.Value);
            Assert.Equal("access-token", returnedToken.AccessToken);
        }

        [Fact]
        public async Task RefreshToken_ReturnsOk_WithTokenResponse()
        {
            // Arrange
            var refreshRequest = new RefreshTokenRequestModel
            {
                UserId = Guid.NewGuid(),
                RefreshToken = "refresh-token"
            };
            var tokenResponse = new TokenResponseModel
            {
                AccessToken = "new-access-token",
                RefreshToken = "new-refresh-token"
            };
            _authCoreServiceMock.Setup(s => s.RefreshTokensAsync(refreshRequest)).ReturnsAsync(tokenResponse);

            // Act
            var result = await _controller.RefreshToken(refreshRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedToken = Assert.IsType<TokenResponseModel>(okResult.Value);
            Assert.Equal("new-access-token", returnedToken.AccessToken);
        }

        [Fact]
        public void AuthenticatedOnlyEndpoint_ReturnsOk()
        {
            // Act
            var result = _controller.AuthenticatedOnlyEndpoint();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("You are authenticated!", okResult.Value);
        }

        [Fact]
        public void AdminOnlyEndpoint_ReturnsOk()
        {
            // Act
            var result = _controller.AdminOnlyEndpoint();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("You are an admin!", okResult.Value);
        }
    }
}
