using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Web.Controllers.WebControllers;

namespace StelexarasApp.Tests.WebControllersTests;

public class PaidiaWebControllerTests
{
    private readonly Mock<IPaidiaService> _mockPaidiaService;
    private readonly Mock<ITeamsService> _mockTeamsService;
    private readonly PaidiaWebController _controller;
    private readonly Mock<ILogger<PaidiaWebController>> _mockLogger;

    public PaidiaWebControllerTests()
    {
        _mockTeamsService = new Mock<ITeamsService>();
        _mockPaidiaService = new Mock<IPaidiaService>();
        _mockLogger = new Mock<ILogger<PaidiaWebController>>();
        _controller = new PaidiaWebController(_mockPaidiaService.Object, _mockTeamsService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithPaidiaList()
    {
        // Arrange
        var paidia = new List<Paidi>
        {
            new Paidi { Id = 1, FullName = "Paidi1", PaidiType = PaidiType.Kataskinotis },
            new Paidi { Id = 2, FullName = "Paidi2", PaidiType = PaidiType.Kataskinotis }
        };

        _mockPaidiaService.Setup(service => service.GetPaidiaInService(PaidiType.Kataskinotis)).ReturnsAsync(paidia);

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Index_ReturnsNotFound_WhenNoPaidiaAvailable()
    {
        // Arrange
        _mockPaidiaService.Setup(service => service.GetPaidiaInService(PaidiType.Kataskinotis)).ReturnsAsync(() => null);

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Index_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        _mockPaidiaService.Setup(service => service.GetPaidiaInService(PaidiType.Kataskinotis)).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }
}
