﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Web.WebControllers;

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

    [Fact]
    public async Task Index_ReturnsViewResult_WithList()
    {
        // Arrange
        var staffList = new List<TomeasDto>
        {
            new TomeasDto { KoinotitesNumber= 5, Name = "Tomeas 1" },
            new TomeasDto { KoinotitesNumber = 2, Name = "Tomeas 2" }
        };

        _mockService.Setup(service => service.GetAllTomeisInService()).ReturnsAsync(staffList);

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<IStelexosDto>>(viewResult.Model);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public async Task Index_ReturnsNotFound_WhenNoTeamsAvailable()
    {
        // Arrange
        _mockService.Setup(service => service.GetAllTomeisInService()).ReturnsAsync((List<TomeasDto>)null);

        // Act
        var result = await _controller.Index();

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("No Tomeis Data", notFoundResult.Value);
    }

    [Fact]
    public async Task Index_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        _mockService.Setup(service => service.GetAllTomeisInService()).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }
}