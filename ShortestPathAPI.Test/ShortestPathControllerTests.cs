using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShortestPathAPI.Controllers;
using ShortestPathAPI.Models;
using ShortestPathAPI.Services.IService;
using Xunit;
using Newtonsoft.Json;

namespace ShortestPathApi.Tests
{
    public class ShortestPathControllerTests
    {
        private readonly Mock<IShortestPathService> _mockService;
        private readonly ShortestPathController _controller;

        public ShortestPathControllerTests()
        {
            // Initialize mock service
            _mockService = new Mock<IShortestPathService>();

            // Inject mock into the controller
            _controller = new ShortestPathController(_mockService.Object);
        }

        [Fact]
        public void CalculateShortestPath_ReturnsOkResult_WithExpectedResult()
        {
            // Arrange
            var mockService = new Mock<IShortestPathService>();
            var request = new GraphRequest { FromNode = "A", ToNode = "D" };
            var result = new ShortestPathData { NodeNames = new List<string> { "A", "B", "F", "G", "D" }, Distance = 10 };
            mockService.Setup(service => service.ShortestPath(request.FromNode, request.ToNode)).Returns(result);
            var controller = new ShortestPathController(mockService.Object);

            // Act
            var response = controller.CalculateShortestPath(request) as OkObjectResult;

            // Assert
            Assert.NotNull(response); Assert.Equal(200, response.StatusCode);
            var responseObject = response.Value as dynamic;
            var splittedArr = responseObject.ToString().Split(",");
            Assert.NotNull(responseObject);
            Assert.Contains("From Node Name = 'A'", splittedArr[1]);
            Assert.Contains("To Node Name = 'D'", splittedArr[1]);
        }

        [Fact]
        public async Task ShortestPath_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            var request = new GraphRequest
            {
                FromNode = "", // Invalid input
                ToNode = "D"
            };

            // Act
            var result = _controller.CalculateShortestPath(request) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid input. Please provide 'FromNode' and 'ToNode'.", result.Value);
        }
    }
}
