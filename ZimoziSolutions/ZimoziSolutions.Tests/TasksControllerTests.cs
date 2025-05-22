using Microsoft.AspNetCore.Mvc;
using Moq;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.ApiModels.Users;
using ZimoziSolutions.Controllers;
using ZimoziSolutions.Core.Interfaces.Tasks;

namespace ZimoziSolutions.Tests
{
    public class TasksControllerTests
    {
        private readonly Mock<ITaskCoreService> _mockService;
        private readonly TasksController _controller;

        public TasksControllerTests()
        {
            _mockService = new Mock<ITaskCoreService>();
            _controller = new TasksController(_mockService.Object);
        }

        [Fact]
        public async Task GetAsync_WithPagerData_ReturnsOkWithPaginatedResponse()
        {
            // Arrange
            var pager = new PagerData { PageNumber = 1, PageSize = 10 };
            var expectedData = new List<TaskModel>
            {
                new TaskModel { TaskId = 1, Description = "Task 1" }
            };
            var paginatedResponse = new PaginatedResponse<TaskModel>(expectedData)
            {
                Page = 1,
                TotalPages = 1,
                TotalCount = 1
            };
            _mockService.Setup(s => s.GetAllAsync(pager, 0)).ReturnsAsync(paginatedResponse);

            // Act
            var result = await _controller.GetAsync(pager);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<PaginatedResponse<TaskModel>>(okResult.Value);
            Assert.Single(value.Data);
            Assert.Equal(1, value.Data[0].TaskId);
            Assert.Equal("Task 1", value.Data[0].Description);
        }

        [Fact]
        public async Task GetAsync_WithId_ReturnsOkWithUserCustomModel()
        {
            // Arrange
            int id = 5;
            var expected = new UserCustomModel { Id = id, Username = "SampleUser" };
            _mockService.Setup(s => s.GetAsync(id)).ReturnsAsync(expected);

            // Act
            var result = await _controller.GetAsync(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<UserCustomModel>(okResult.Value);
            Assert.Equal(id, value.Id);
            Assert.Equal("SampleUser", value.Username);
        }

        [Fact]
        public async Task GetUserAsync_WithUserId_ReturnsOkWithPaginatedResponse()
        {
            // Arrange
            int userId = 2;
            var pager = new PagerData { PageNumber = 1, PageSize = 10 };
            var expectedData = new List<TaskModel>
            {
                new TaskModel { TaskId = 2, Description = "User Task" }
            };
            var paginatedResponse = new PaginatedResponse<TaskModel>(expectedData)
            {
                Page = 1,
                TotalPages = 1,
                TotalCount = 1
            };
            _mockService.Setup(s => s.GetListFilteredByAssignedUserId(pager, userId)).ReturnsAsync(paginatedResponse);

            // Act
            var result = await _controller.GetUserAsync(userId, pager);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<PaginatedResponse<TaskModel>>(okResult.Value);
            Assert.Single(value.Data);
            Assert.Equal(2, value.Data[0].TaskId);
            Assert.Equal("User Task", value.Data[0].Description);
        }

        [Fact]
        public async Task PostAsync_ValidModel_ReturnsOkWithGenericDataRecord()
        {
            // Arrange
            var model = new TaskModel { TaskId = 0, Description = "New Task" };
            var expected = new GenericDataRecord { RecordId = 10 };
            _mockService.Setup(s => s.AddAsync(model)).ReturnsAsync(expected);

            // Act
            var result = await _controller.PostAsync(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<GenericDataRecord>(okResult.Value);
            Assert.Equal(10, value.RecordId);
        }

        [Fact]
        public async Task PutAsync_ValidModel_ReturnsOkWithGenericDataRecord()
        {
            // Arrange
            var model = new TaskModel { TaskId = 2, Description = "Updated Task" };
            var expected = new GenericDataRecord { RecordId = 2 };
            _mockService.Setup(s => s.UpdateAsync(model)).ReturnsAsync(expected);

            // Act
            var result = await _controller.PutAsync(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<GenericDataRecord>(okResult.Value);
            Assert.Equal(2, value.RecordId);
        }

        [Fact]
        public async Task Delete_WithId_ReturnsOkWithGenericDataRecord()
        {
            // Arrange
            int id = 3;
            var expected = new GenericDataRecord { RecordId = id };
            _mockService.Setup(s => s.DeleteAsync(id)).ReturnsAsync(expected);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<GenericDataRecord>(okResult.Value);
            Assert.Equal(id, value.RecordId);
        }
    }
}
