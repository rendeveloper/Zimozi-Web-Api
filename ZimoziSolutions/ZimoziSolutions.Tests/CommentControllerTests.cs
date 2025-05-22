using Microsoft.AspNetCore.Mvc;
using Moq;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.Controllers;
using ZimoziSolutions.Core.Interfaces.TaskComments;

namespace ZimoziSolutions.Tests
{
    public class CommentControllerTests
    {
        private readonly Mock<ITaskCommentsCoreService> _mockService;
        private readonly CommentController _controller;

        public CommentControllerTests()
        {
            _mockService = new Mock<ITaskCommentsCoreService>();
            _controller = new CommentController(_mockService.Object);
        }

        [Fact]
        public async Task GetAsync_WithPagerData_ReturnsOkWithPaginatedResponse()
        {
            // Arrange
            var pager = new PagerData { PageNumber = 1, PageSize = 10 };
            var expectedData = new List<TaskCommentsModel>
            {
                new TaskCommentsModel { Id = 1, Comments = "Test comment" }
            };
            var paginatedResponse = new PaginatedResponse<TaskCommentsModel>(expectedData)
            {
                Page = 1,
                TotalPages = 1,
                TotalCount = 1
            };
            _mockService.Setup(s => s.GetAllAsync(pager)).ReturnsAsync(paginatedResponse);

            // Act
            var result = await _controller.GetAsync(pager);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<PaginatedResponse<TaskCommentsModel>>(okResult.Value);
            Assert.Single(value.Data);
            Assert.Equal(1, value.Data[0].Id);
            Assert.Equal("Test comment", value.Data[0].Comments);
        }

        [Fact]
        public async Task GetAsync_WithId_ReturnsOkWithTaskCommentsModel()
        {
            // Arrange
            int id = 5;
            var expected = new TaskCommentsModel { Id = id, Comments = "Sample" };
            _mockService.Setup(s => s.GetAsync(id)).ReturnsAsync(expected);

            // Act
            var result = await _controller.GetAsync(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<TaskCommentsModel>(okResult.Value);
            Assert.Equal(id, value.Id);
            Assert.Equal("Sample", value.Comments);
        }

        [Fact]
        public async Task PostAsync_ValidModel_ReturnsOkWithGenericDataRecord()
        {
            // Arrange
            var model = new TaskCommentsModel { Id = 0, Comments = "New comment" };
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
            var model = new TaskCommentsModel { Id = 2, Comments = "Updated comment" };
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
