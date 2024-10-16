using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Web.ApiControllers;

namespace StelexarasApp.Tests.WebControllersTests;

public class DutiesWebControllerTests
{
    private readonly Mock<IDutyService> _mockService;
    private readonly DutiesController _controller;
    private readonly Mock<ILogger<DutiesController>> _mockLogger;

    public DutiesWebControllerTests()
    {
        _mockService = new Mock<IDutyService>();
        _mockLogger = new Mock<ILogger<DutiesController>>();
        _controller = new DutiesController(_mockService.Object);
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithDutiesList()
    {
        // Arrange
        var dutiesList = new List<Duty>
        {
            new Duty { Id = 1, Name = "Description 1",Date = DateTime.Now },
            new Duty { Id = 2, Name= "Description 2" ,Date = DateTime.Now },
        };

        _mockService.Setup(service => service.GetDutiesInService()).ReturnsAsync(dutiesList);

        // Act
        var result = await _controller.GetDuties();

        // Assert
        var viewResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<IEnumerable<Duty>>(viewResult.Value);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public async Task Index_ReturnsNotFound_WhenNoDutiesAvailable()
    {
        // Arrange
        _mockService.Setup(service => service.GetDutiesInService()).ReturnsAsync((List<Duty>)null);

        // Act
        var result = await _controller.GetDuties();

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Index_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        _mockService.Setup(service => service.GetDutiesInService()).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.GetDuties();

        // Assert
        var viewResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, viewResult.StatusCode);
    }
}
