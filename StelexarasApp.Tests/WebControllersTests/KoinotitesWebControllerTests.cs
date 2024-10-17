using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Web.WebControllers;

namespace StelexarasApp.Tests.WebControllersTests;

public class KoinotitesWebControllerTests
{
    private readonly Mock<ITeamsService> _mockService;
    private readonly KoinotitesWebController _controller;
    private readonly Mock<ILogger<KoinotitesWebController>> _mockLogger;

    public KoinotitesWebControllerTests()
    {
        _mockService = new Mock<ITeamsService>();
        _mockLogger = new Mock<ILogger<KoinotitesWebController>>();
        _controller = new KoinotitesWebController(_mockService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithList()
    {
        // Arrange
        var koinotites = new List<KoinotitaDto>
        {
            new KoinotitaDto { Id = 1, Name = "Koinotita1", TomeasName ="TomeasName1" },
            new KoinotitaDto { Id = 2, Name = "Koinotita2" , TomeasName ="TomeasName1" },
        };

        _mockService.Setup(service => service.GetAllKoinotitesInService()).ReturnsAsync(koinotites);

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<KoinotitaDto>>(viewResult.Model);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public async Task Index_ReturnsNotFound_WhenNoTeamsAvailable()
    {
        // Arrange
        _mockService.Setup(service => service.GetAllKoinotitesInService()).ReturnsAsync(() => null);

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("No Koinotites Data", viewResult.Value);
    }

    [Fact]
    public async Task Index_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        _mockService.Setup(service => service.GetAllKoinotitesInService()).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }
}
