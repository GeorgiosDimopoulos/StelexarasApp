using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Web.Controllers.WebControllers;

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

    [Fact]
    public async Task Details_ReturnsViewResult_WithKoinotita()
    {
        // Arrange
        var koinotitaName = "TestKoinotita";
        var koinotita = new KoinotitaDto { Name = koinotitaName, TomeasName = "TestTomeas", SkinesNumber = 5 };
        _mockService.Setup(service => service.GetKoinotitaByNameInService(koinotitaName)).ReturnsAsync(koinotita);

        // Act
        var result = await _controller.Details(koinotitaName);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<KoinotitaDto>(viewResult.ViewData.Model);
        Assert.Equal(koinotitaName, model.Name);
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenKoinotitaNotFound()
    {
        // Arrange
        var koinotitaName = "NonExistentKoinotita";
        _mockService.Setup(service => service.GetKoinotitaByNameInService(koinotitaName)).ReturnsAsync((KoinotitaDto)null);

        // Act
        var result = await _controller.Details(koinotitaName);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Koinotita not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task Details_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        var koinotitaName = "TestKoinotita";
        _mockService.Setup(service => service.GetKoinotitaByNameInService(koinotitaName)).ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.Details(koinotitaName);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }

    [Fact]
    public async Task Edit_ReturnsViewResult_WithKoinotita()
    {
        // Arrange
        var koinotitaName = "TestKoinotita";
        var koinotita = new KoinotitaDto { Name = koinotitaName, TomeasName = "TestTomeas", SkinesNumber = 5 };
        _mockService.Setup(service => service.GetKoinotitaByNameInService(koinotitaName)).ReturnsAsync(koinotita);

        // Act
        var result = await _controller.Edit(koinotitaName);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<KoinotitaDto>(viewResult.ViewData.Model);
        Assert.Equal(koinotitaName, model.Name);
    }

    [Fact]
    public async Task Edit_ReturnsNotFound_WhenKoinotitaNotFound()
    {
        // Arrange
        var koinotitaName = "NonExistentKoinotita";
        _mockService.Setup(service => service.GetKoinotitaByNameInService(koinotitaName)).ReturnsAsync((KoinotitaDto)null);

        // Act
        var result = await _controller.Edit(koinotitaName);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Koinotita not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task Edit_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        var koinotitaName = "TestKoinotita";
        _mockService.Setup(service => service.GetKoinotitaByNameInService(koinotitaName)).ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.Edit(koinotitaName);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }

    [Fact]
    public async Task Delete_ReturnsViewResult_WithKoinotita()
    {
        // Arrange
        var koinotitaName = "TestKoinotita";
        var koinotita = new KoinotitaDto { Name = koinotitaName, TomeasName = "TestTomeas", SkinesNumber = 5 };
        _mockService.Setup(service => service.GetKoinotitaByNameInService(koinotitaName)).ReturnsAsync(koinotita);

        // Act
        var result = await _controller.Delete(koinotitaName);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<KoinotitaDto>(viewResult.ViewData.Model);
        Assert.Equal(koinotitaName, model.Name);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenKoinotitaNotFound()
    {
        // Arrange
        var koinotitaName = "NonExistentKoinotita";
        _mockService.Setup(service => service.GetKoinotitaByNameInService(koinotitaName)).ReturnsAsync((KoinotitaDto)null);

        // Act
        var result = await _controller.Delete(koinotitaName);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Koinotita not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task Delete_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        var koinotitaName = "TestKoinotita";
        _mockService.Setup(service => service.GetKoinotitaByNameInService(koinotitaName)).ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.Delete(koinotitaName);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Name", "Required");

        // Act
        var result = await _controller.Create(new KoinotitaDto() { Name = "KoinotitaDtoName", TomeasName = "TomeasName" });

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.IsType<SerializableError>(badRequestResult.Value);
    }

    [Fact]
    public async Task Create_ReturnsNotFound_WhenKoinotitaNotCreated()
    {
        // Arrange
        var koinotita = new KoinotitaDto { Name = "TestKoinotita", TomeasName = "TestTomeas", SkinesNumber = 5 };
        _mockService.Setup(service => service.AddKoinotitaInService(koinotita)).ReturnsAsync(false);

        // Act
        var result = await _controller.Create(koinotita);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Koinotita not created.", notFoundResult.Value);
    }

    [Fact]
    public async Task Create_RedirectsToIndex_WhenKoinotitaCreated()
    {
        // Arrange
        var koinotita = new KoinotitaDto { Name = "TestKoinotita", TomeasName = "TestTomeas", SkinesNumber = 5 };
        _mockService.Setup(service => service.AddKoinotitaInService(koinotita)).ReturnsAsync(true);

        // Act
        var result = await _controller.Create(koinotita);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(KoinotitesWebController.Index), redirectToActionResult.ActionName);
    }

    [Fact]
    public async Task Create_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        var koinotita = new KoinotitaDto { Name = "TestKoinotita", TomeasName = "TestTomeas", SkinesNumber = 5 };
        _mockService.Setup(service => service.AddKoinotitaInService(koinotita)).ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.Create(koinotita);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }
}