using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Web.Controllers.WebControllers;

namespace StelexarasApp.Tests.WebControllersTests;

public class SkinesWebControllerTests
{
    private readonly Mock<ITeamsService> _mockService;
    private readonly SkinesWebController _controller;
    private readonly Mock<ILogger<SkinesWebController>> _mockLogger;

    public SkinesWebControllerTests()
    {
        _mockService = new Mock<ITeamsService>();
        _mockLogger = new Mock<ILogger<SkinesWebController>>();
        _controller = new SkinesWebController(_mockService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithAllTeams()
    {
        // Arrange
        var skines = new List<SkiniDto> { new SkiniDto { Name = "Skini1" } };
        var koinotites = new List<KoinotitaDto> { new KoinotitaDto { Name = "Koinotita1", TomeasName = "TomeasName" } };
        var tomeis = new List<TomeasDto> { new TomeasDto { Name = "Tomeas1" } };
        _mockService.Setup(service => service.GetAllSkinesInService(new())).ReturnsAsync(skines);
        _mockService.Setup(service => service.GetAllKoinotitesInService(new())).ReturnsAsync(koinotites);
        _mockService.Setup(service => service.GetAllTomeisInService(new())).ReturnsAsync(tomeis);

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<object>>(viewResult.ViewData.Model);
        Assert.Equal(3, model.Count());
    }

    [Fact]
    public async Task Index_ReturnsNotFound_WhenNoTeamsFound()
    {
        // Arrange
        _mockService.Setup(service => service.GetAllSkinesInService(new())).ReturnsAsync((IEnumerable<SkiniDto>)null);
        _mockService.Setup(service => service.GetAllKoinotitesInService(new())).ReturnsAsync((IEnumerable<KoinotitaDto>)null);
        _mockService.Setup(service => service.GetAllTomeisInService(new())).ReturnsAsync((IEnumerable<TomeasDto>)null);

        // Act
        var result = await _controller.Index();

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Not all Teams Data", notFoundResult.Value);
    }

    [Fact]
    public async Task Index_ReturnsNothing_WhenExceptionThrown()
    {
        // Arrange
        _mockService.Setup(service => service.GetAllSkinesInService(new())).Returns(() => throw new Exception());

        // Act
        var result = await _controller.Index();

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
        var result = await _controller.Create(new SkiniDto());

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.IsType<SerializableError>(badRequestResult.Value);
    }

    [Fact]
    public async Task Create_ReturnsNotFound_WhenSkiniNotCreated()
    {
        // Arrange
        var skini = new SkiniDto { Name = "TestSkini" };
        _mockService.Setup(service => service.AddSkiniInService(skini)).ReturnsAsync(false);

        // Act
        var result = await _controller.Create(skini);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Skini not created.", notFoundResult.Value);
    }

    [Fact]
    public async Task Create_RedirectsToIndex_WhenSkiniCreated()
    {
        // Arrange
        var skini = new SkiniDto { Name = "TestSkini" };
        _mockService.Setup(service => service.AddSkiniInService(skini)).ReturnsAsync(true);

        // Act
        var result = await _controller.Create(skini);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(SkinesWebController.Index), redirectToActionResult.ActionName);
    }

    [Fact]
    public async Task Create_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        var skini = new SkiniDto { Name = "TestSkini" };
        _mockService.Setup(service => service.AddSkiniInService(skini)).ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.Create(skini);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }

    [Fact]
    public async Task Delete_ReturnsViewResult_WithSkini()
    {
        // Arrange
        var skiniName = "TestSkini";
        var skini = new SkiniDto { Name = skiniName };
        _mockService.Setup(service => service.GetSkiniByNameInService(new(), skiniName)).ReturnsAsync(skini);

        // Act
        var result = await _controller.Delete(skiniName);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<SkiniDto>(viewResult.ViewData.Model);
        Assert.Equal(skiniName, model.Name);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenSkiniNotFound()
    {
        // Arrange
        var skiniName = "NonExistentSkini";
        _mockService.Setup(service => service.GetSkiniByNameInService(new(), skiniName)).ReturnsAsync((SkiniDto)null);

        // Act
        var result = await _controller.Delete(skiniName);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Skini not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task Delete_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        var skiniName = "TestSkini";
        _mockService.Setup(service => service.GetSkiniByNameInService(new(), skiniName)).ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.Delete(skiniName);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }

    [Fact]
    public async Task Edit_ReturnsViewResult_WithSkini()
    {
        // Arrange
        var skiniName = "TestSkini";
        var skini = new SkiniDto { Name = skiniName };
        _mockService.Setup(service => service.GetSkiniByNameInService(new(), skiniName)).ReturnsAsync(skini);

        // Act
        var result = await _controller.Edit(skiniName);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<SkiniDto>(viewResult.ViewData.Model);
        Assert.Equal(skiniName, model.Name);
    }

    [Fact]
    public async Task Edit_ReturnsNotFound_WhenSkiniNotFound()
    {
        // Arrange
        var skiniName = "NonExistentSkini";
        _mockService.Setup(service => service.GetSkiniByNameInService(new(), skiniName)).ReturnsAsync((SkiniDto)null);

        // Act
        var result = await _controller.Edit(skiniName);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Skini not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task Edit_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        var skiniName = "TestSkini";
        _mockService.Setup(service => service.GetSkiniByNameInService(new(), skiniName)).ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.Edit(skiniName);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }
}
