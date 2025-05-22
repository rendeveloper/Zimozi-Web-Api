using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.ApiModels.Tasks;
using ZimoziSolutions.Controllers;
using ZimoziSolutions.Core.Interfaces.TaskNotifs;

namespace ZimoziSolutions.Tests
{
    public class NotificationControllerTests
    {
        private readonly Mock<INotificationsCoreService> _mockService;
        private readonly NotificationController _controller;

        public NotificationControllerTests()
        {
            _mockService = new Mock<INotificationsCoreService>();
            _controller = new NotificationController(_mockService.Object);
        }

        [Fact]
        public async Task GetAsync_WithPagerData_ReturnsOkWithPaginatedResponse()
        {
            // Arrange
            var pager = new PagerData { PageNumber = 1, PageSize = 10 };
            var expectedData = new List<NotificationsModel>
            {
                new NotificationsModel { Id = 1, TaskUpdates = "Update 1" }
            };
            var paginatedResponse = new PaginatedResponse<NotificationsModel>(expectedData)
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
            var value = Assert.IsType<PaginatedResponse<NotificationsModel>>(okResult.Value);
            Assert.Single(value.Data);
            Assert.Equal(1, value.Data[0].Id);
            Assert.Equal("Update 1", value.Data[0].TaskUpdates);
        }

        [Fact]
        public async Task GetAsync_WithId_ReturnsOkWithNotificationsModel()
        {
            // Arrange
            int id = 5;
            var expected = new NotificationsModel { Id = id, TaskUpdates = "Sample update" };
            _mockService.Setup(s => s.GetAsync(id)).ReturnsAsync(expected);

            // Act
            var result = await _controller.GetAsync(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<NotificationsModel>(okResult.Value);
            Assert.Equal(id, value.Id);
            Assert.Equal("Sample update", value.TaskUpdates);
        }

        [Fact]
        public async Task PostAsync_ValidModel_ReturnsOkWithGenericDataRecord()
        {
            // Arrange
            var model = new NotificationsModel { Id = 0, TaskUpdates = "New notification" };
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
            var model = new NotificationsModel { Id = 2, TaskUpdates = "Updated notification" };
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
