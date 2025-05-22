using Microsoft.AspNetCore.Mvc;
using Moq;
using ZimoziSolutions.ApiModels.Pager;
using ZimoziSolutions.ApiModels.Responses;
using ZimoziSolutions.ApiModels.Users;
using ZimoziSolutions.Controllers;
using ZimoziSolutions.Core.Interfaces.Users;

namespace ZimoziSolutions.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserCoreService> _mockService;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockService = new Mock<IUserCoreService>();
            _controller = new UserController(_mockService.Object);
        }

        [Fact]
        public async Task GetAsync_WithPagerData_ReturnsOk()
        {
            var pager = new PagerData();
            var paginatedResponse = new PaginatedResponse<UserCustomModel>();
            _mockService.Setup(s => s.GetAllAsync(pager, ""))
                .ReturnsAsync(paginatedResponse);

            var result = await _controller.GetAsync(pager);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAsync_WithId_ReturnsOk()
        {
            var user = new UserCustomModel();
            _mockService.Setup(s => s.GetAsync(1))
                .ReturnsAsync(user);

            var result = await _controller.GetAsync(1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task PostAsync_ReturnsOk()
        {
            var user = new UserCustomModel();
            var record = new GenericDataRecord();
            _mockService.Setup(s => s.AddAsync(user))
                .ReturnsAsync(record);

            var result = await _controller.PostAsync(user);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task PutAsync_ReturnsOk()
        {
            var user = new UserCustomModel();
            var record = new GenericDataRecord();
            _mockService.Setup(s => s.UpdateAsync(user))
                .ReturnsAsync(record);

            var result = await _controller.PutAsync(user);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            var record = new GenericDataRecord();
            _mockService.Setup(s => s.DeleteAsync(1))
                .ReturnsAsync(record);

            var result = await _controller.Delete(1);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
