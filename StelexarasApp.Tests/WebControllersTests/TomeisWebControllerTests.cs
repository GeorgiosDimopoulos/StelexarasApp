﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Web.Controllers.WebControllers;

namespace StelexarasApp.Tests.WebControllersTests;

public class TomeisWebControllerTests
{
    private readonly Mock<ITeamsService> _mockService;
    private readonly TomeisWebController _controller;
    private readonly Mock<ILogger<TomeisWebController>> _mockLogger;

    public TomeisWebControllerTests()
    {
        _mockService = new Mock<ITeamsService>();
        _mockLogger = new Mock<ILogger<TomeisWebController>>();
        _controller = new TomeisWebController(_mockService.Object, _mockLogger.Object);
    }

    [Fact(Skip = "Temporarily ignoring this test")]
    public async Task Index_ReturnsViewResult_WithList()
    {
        // Arrange
        var tomeisList = new List<TomeasDto>
        {
            new () { KoinotitesNumber= 5, Name = "Tomeas 1" },
            new (){ KoinotitesNumber = 2, Name = "Tomeas 2" }
        };

        _mockService.Setup(service => service.GetAllTomeisInService(new())).ReturnsAsync(tomeisList);

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<TomeasDto>>(viewResult.Model);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public async Task Index_ReturnsNotFound_WhenNoTeamsAvailable()
    {
        // Arrange
        _mockService.Setup(service => service.GetAllTomeisInService(new())).ReturnsAsync((List<TomeasDto>)null);

        // Act
        var result = await _controller.Index();

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("No Tomeis Data", notFoundResult.Value);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Name", "Required");

        // Act
        var result = await _controller.Create(new TomeasDto());

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.IsType<SerializableError>(badRequestResult.Value);
    }

    [Fact]
    public async Task Create_ReturnsNotFound_WhenTomeasNotCreated()
    {
        // Arrange
        var tomeas = new TomeasDto { Name = "TestTomeas", KoinotitesNumber = 5 };
        _mockService.Setup(service => service.AddTomeasInService(tomeas)).ReturnsAsync(false);

        // Act
        var result = await _controller.Create(tomeas);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Tomeis not created.", notFoundResult.Value);
    }

    [Fact]
    public async Task Create_RedirectsToIndex_WhenTomeasCreated()
    {
        // Arrange
        var tomeas = new TomeasDto { Name = "TestTomeas", KoinotitesNumber = 5 };
        _mockService.Setup(service => service.AddTomeasInService(tomeas)).ReturnsAsync(true);

        // Act
        var result = await _controller.Create(tomeas);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(TomeisWebController.Index), redirectToActionResult.ActionName);
    }

    [Fact]
    public async Task Create_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        var tomeas = new TomeasDto { Name = "TestTomeas", KoinotitesNumber = 5 };
        _mockService.Setup(service => service.AddTomeasInService(tomeas)).ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.Create(tomeas);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }

    [Fact(Skip = "Temporarily ignoring this test")]
    public async Task Index_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        _mockService.Setup(service => service.GetAllTomeisInService(new())).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }

    [Fact(Skip = "Temporarily ignoring this test")]
    public async Task Edit_ReturnsViewResult_WithTomeas()
    {
        // Arrange
        var tomeasId = "TestTomeas";
        var tomeas = new TomeasDto { Name = tomeasId, KoinotitesNumber = 5 };
        _mockService.Setup(service => service.GetTomeaByNameInService(new(), tomeasId)).ReturnsAsync(tomeas);

        // Act
        var result = await _controller.Edit(tomeasId);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<TomeasDto>(viewResult.ViewData.Model);
        Assert.Equal(tomeasId, model.Name);
    }

    [Fact]
    public async Task Edit_ReturnsNotFound_WhenTomeasNotFound()
    {
        // Arrange
        var tomeasId = "NonExistentTomeas";
        _mockService.Setup(service => service.GetTomeaByNameInService(new(),tomeasId)).ReturnsAsync((TomeasDto)null);

        // Act
        var result = await _controller.Edit(tomeasId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Tomeis not found.", notFoundResult.Value);
    }

    [Fact(Skip = "Temporarily ignoring this test")]
    public async Task Edit_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        var tomeasId = "TestTomeas";
        _mockService.Setup(service => service.GetTomeaByNameInService(new(), tomeasId)).ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.Edit(tomeasId);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }

    [Fact(Skip = "Temporarily ignoring this test")]
    public async Task Delete_ReturnsViewResult_WithTomeas()
    {
        // Arrange
        var tomeasId = "TestTomeas";
        var tomeas = new TomeasDto { Name = tomeasId, KoinotitesNumber = 5 };
        _mockService.Setup(service => service.GetTomeaByNameInService(new(), tomeasId)).ReturnsAsync(tomeas);

        // Act
        var result = await _controller.Delete(tomeasId);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<TomeasDto>(viewResult.ViewData.Model);
        Assert.Equal(tomeasId, model.Name);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenTomeasNotFound()
    {
        // Arrange
        var tomeasId = "NonExistentTomeas";
        _mockService.Setup(service => service.GetTomeaByNameInService(new(),tomeasId)).ReturnsAsync((TomeasDto)null);

        // Act
        var result = await _controller.Delete(tomeasId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Tomeis not found.", notFoundResult.Value);
    }

    [Fact(Skip = "Temporarily ignoring this test")]
    public async Task Delete_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        var tomeasId = "TestTomeas";
        _mockService.Setup(service => service.GetTomeaByNameInService(new(), tomeasId)).ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.Delete(tomeasId);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }
}
