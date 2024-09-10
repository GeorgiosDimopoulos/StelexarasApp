using AutoMapper;
using Moq;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.Services;

namespace StelexarasApp.Tests.ServicesTests;

public class TeamsServiceTests
{
    private readonly Mock<ITeamsRepository> _mockdteamsRepository;
    private readonly TeamsService _teamsService;
    private readonly Mock<IMapper> _mockMapper;

    public TeamsServiceTests()
    {
        _mockdteamsRepository = new Mock<ITeamsRepository>();
        _mockMapper = new Mock<IMapper>();
        _teamsService = new TeamsService(_mockMapper.Object, _mockdteamsRepository.Object);
    }

    [Fact]
    public async Task AddSkiniInService_ShouldReturnTrue()
    {
        // Arrange
        var team = new SkiniDto { Id = 1, Name = "TestTeam" };
        _mockdteamsRepository.Setup(m => m.AddSkiniInDb(It.IsAny<Skini>())).ReturnsAsync(true);

        // Act
        var result = await _teamsService.AddSkiniInService(team);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteSkiniInService_ShouldReturnTrue()
    {
        // Arrange
        var team = new Skini { Id = 1, Name = "TestTeam" };
        _mockdteamsRepository.Setup(m => m.DeleteSkiniInDb(It.IsAny<int>())).ReturnsAsync(true);

        // Act
        var result = await _teamsService.DeleteSkiniInService(team.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetSkinesInService_ShouldReturnTeams()
    {
        // Arrange
        var teams = new List<Skini>
        {
            new Skini { Id = 1, Name = "TestTeam1"},
            new Skini { Id = 2, Name = "TestTeam2" }
        };
        _mockdteamsRepository.Setup(m => m.GetSkinesInDb()).ReturnsAsync(teams);

        // Act
        var result = await _teamsService.GetSkines();
    }

    [Fact]
    public async Task UpdateSkiniInService_ShouldReturnTeam()
    {
        // Arrange
        var team = new SkiniDto { Id = 1, Name = "TestTeam"};
        _mockdteamsRepository.Setup(m => m.UpdateSkiniInDb(It.IsAny<Skini>())).ReturnsAsync(true);

        // Act
        var result = await _teamsService.UpdateSkiniInService(team);

        // Assert
        Assert.NotNull(result);
        Assert.True(result);
    }
}
