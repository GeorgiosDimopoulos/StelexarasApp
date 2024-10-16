using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Web.WebControllers;

namespace StelexarasApp.Tests.WebControllersTests;

public class StaffWebControllerTests
{
    private readonly Mock<IStaffService> _mockStaffService;
    private readonly StaffWebController _controller;
    private readonly Mock<ILogger<StaffWebController>> _mockLogger;

    public StaffWebControllerTests()
    {
        _mockStaffService = new Mock<IStaffService>();
        _mockLogger = new Mock<ILogger<StaffWebController>>();
        _controller = new StaffWebController(_mockStaffService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithStaffList()
    {
        // Arrange
        var staffList = new List<IStelexosDto>
        {
            new OmadarxisDto { Id = 1, FullName = "John Doe", Thesi = Thesi.Omadarxis },
            new KoinotarxisDto { Id = 2, FullName = "Jane Doe", Thesi = Thesi.Koinotarxis }
        };

        _mockStaffService.Setup(service => service.GetAllStaffInService()).ReturnsAsync(staffList);

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<IStelexosDto>>(viewResult.Model);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public async Task Index_ReturnsNotFound_WhenNoStaffAvailable()
    {
        // Arrange
        _mockStaffService.Setup(service => service.GetAllStaffInService()).ReturnsAsync((List<IStelexosDto>)null);

        // Act
        var result = await _controller.Index();

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("No staff data available.", notFoundResult.Value);
    }

    [Fact]
    public async Task Index_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        _mockStaffService.Setup(service => service.GetAllStaffInService()).ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }
}
