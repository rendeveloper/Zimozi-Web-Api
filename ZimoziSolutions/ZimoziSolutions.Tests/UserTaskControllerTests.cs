using Microsoft.AspNetCore.Mvc;
using Moq;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.ApiModels.UserTask;
using ZimoziSolutions.Controllers;
using ZimoziSolutions.Core.Interfaces.UserTask;

namespace ZimoziSolutions.Tests
{
    public class UserTaskControllerTests
    {
        private readonly Mock<IUserTasksCoreService> _mockService;
        private readonly UserTaskController _controller;

        public UserTaskControllerTests()
        {
            _mockService = new Mock<IUserTasksCoreService>();
            _controller = new UserTaskController(_mockService.Object);
        }
        [Fact]
        public async Task GetUserAsync_ReturnsOkResult()
        {
            // Arrange
            var pager = new PagerData();
            int userId = 1;
            var expected = new PaginatedResponse<TaskModel>(new List<TaskModel>());
            _mockService.Setup(s => s.GetAllAsync(pager, userId)).ReturnsAsync(expected);

            // Act
            var result = await _controller.GetUserAsync(userId, pager);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public async Task PostAsync_ReturnsOkResult()
        {
            // Arrange
            var model = new UserTasksModel();
            var expected = new GenericDataRecord { RecordId = 1 };
            _mockService.Setup(s => s.AddAsync(model)).ReturnsAsync(expected);

            // Act
            var result = await _controller.PostAsync(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public async Task PutAsync_ReturnsOkResult()
        {
            // Arrange
            var model = new UserTasksModel();
            var expected = new GenericDataRecord { RecordId = 2 };
            _mockService.Setup(s => s.UpdateAsync(model)).ReturnsAsync(expected);

            // Act
            var result = await _controller.PutAsync(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult()
        {
            // Arrange
            int id = 1;
            var expected = new GenericDataRecord { RecordId = 3 };
            _mockService.Setup(s => s.DeleteAsync(id)).ReturnsAsync(expected);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, okResult.Value);
        }
    }
}
