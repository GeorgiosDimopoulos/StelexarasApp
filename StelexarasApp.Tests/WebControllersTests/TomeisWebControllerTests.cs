using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Web.ApiControllers;
using StelexarasApp.Web.WebControllers;

namespace StelexarasApp.Tests.WebControllersTests;

public class TomeisWebControllerTests
{
    private readonly Mock<ITeamsService> _mockService;
    private readonly TeamsController _controller;
    private readonly ILogger _logger;
    public TomeisWebControllerTests()
    {
        _mockService = new Mock<ITeamsService>();
        _logger = new Mock<ILogger<TomeisWebController>>().Object;
        _controller = new TomeisWebController(_mockService.Object, _logger);
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithList()
    {
    }

    [Fact]
    public async Task Index_ReturnsNotFound_WhenNoTeamsAvailable()
    {
    }

    [Fact]
    public async Task Index_ReturnsErrorView_WhenExceptionThrown()
    {
    }
}
