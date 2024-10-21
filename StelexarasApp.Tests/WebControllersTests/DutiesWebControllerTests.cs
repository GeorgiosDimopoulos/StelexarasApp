using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Web.Controllers.WebControllers;

namespace StelexarasApp.Tests.WebControllersTests;

public class DutiesWebControllerTests
{
    private readonly Mock<IDutyService> _mockService;
    private readonly DutiesWebController _controller;
    private readonly Mock<ILogger<DutiesWebController>> _mockLogger;

    public DutiesWebControllerTests()
    {
        _mockService = new Mock<IDutyService>();
        _mockLogger = new Mock<ILogger<DutiesWebController>>();
        _controller = new DutiesWebController(_mockService.Object);
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithDutiesList()
    {
        // Arrange
        var duties = new List<Duty>
        {
            new Duty { Id = 1, Name = "Duty 1", Date = DateTime.Now },
            new Duty { Id = 2, Name = "Duty 2", Date = DateTime.Now },
            new Duty { Id = 3, Name = "Duty 3", Date = DateTime.Now }
        };

        _mockService.Setup(service => service.GetDutiesInService()).ReturnsAsync(duties);

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Duty>>(viewResult.Model);
        Assert.Equal(3, model.Count());
    }

    [Fact]
    public async Task Index_ReturnsNotFound_WhenNoDutiesAvailable()
    {
        // Arrange
        _mockService.Setup(service => service.GetDutiesInService()).ReturnsAsync(() => null);

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(404, viewResult.StatusCode);
    }

    [Fact]
    public async Task Index_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        _mockService.Setup(service => service.GetDutiesInService()).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }

    [Fact]
    public async Task Details_ReturnsViewResult_WithDuty()
    {
        // Arrange
        var duty = new Duty { Id = 1, Name = "Duty 1", Date = DateTime.Now };
        _mockService.Setup(service => service.GetDutyByIdInService(duty.Id));

        // Act
        var result = await _controller.Details(duty.Id);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
    }
}
