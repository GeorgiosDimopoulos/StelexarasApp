using AutoMapper;
using Moq;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Library.Models.Domi;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Services.Services;
using StelexarasApp.Library.Models.Atoma;

namespace StelexarasApp.Tests.ServicesTests;

public class TeamsServiceTests
{
    private readonly Mock<ITeamsRepository> _mockdteamsRepository;
    private readonly ITeamsService _teamsService;
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
        var team = new SkiniDto { Id = 1, Name = "TestTeam", KoinotitaName = "KoinotitaName", PaidiaNumber = 1, Sex = Sex.Female };
        _mockdteamsRepository.Setup(m => m.AddSkiniInDb(It.IsAny<Skini>())).ReturnsAsync(true);

        // Act
        var result = await _teamsService.AddSkiniInService(team);

        // Assert
        Assert.True(result);
        _mockdteamsRepository.Verify(m => m.AddSkiniInDb(It.IsAny<Skini>()), Times.Once);
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
        _mockdteamsRepository.Setup(m => m.GetSkinesInDb(new())).ReturnsAsync(teams);

        // Act
        var result = await _teamsService.GetAllSkinesInService(new());

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetAllTomeisInService_ShouldReturnTeams()
    {
        // Arrange
        var teams = new List<Tomeas>
        {
            new Tomeas { Id = 1, Name = "TestTomeas1" },
            new Tomeas { Id = 2, Name = "TestTomeas2" }
        };
        _mockdteamsRepository.Setup(m => m.GetTomeisInDb(new())).ReturnsAsync(teams);

        // Act
        var result = await _teamsService.GetAllTomeisInService(new());

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetAllKoinotitesInService_ShouldReturnTeams()
    {
        // Arrange
        var teams = new List<Koinotita>
        {
            new Koinotita
            {
                Id = 1, Name = "TestKoinotita1", Koinotarxis = new Koinotarxis
                {
                    FullName = "Test Name1" , Tel = "123456790" , Sex = Sex.Female, Thesi = Thesi.Koinotarxis, Age = 30
                }
            },
            new Koinotita
            { Id = 2, Name = "TestKoinotita2", Koinotarxis = new Koinotarxis()
            {
                FullName = "Test Name2", Tel = "1334567890", Sex = Sex.Female, Thesi = Thesi.Koinotarxis, Age = 30
            }
            }
        };
        _mockdteamsRepository.Setup(m => m.GetKoinotitesInDb(new())).ReturnsAsync(teams);

        // Act
        var result = await _teamsService.GetAllKoinotitesInService(new());

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetKoinotitesAnaTomeaInService_ShouldReturnTeams()
    {
        // Arrange
        var tomeasId = 1;
        var teams = new List<Koinotita>
        {
            new Koinotita
            {
                Id = 1, Name = "TestKoinotita1", Koinotarxis = new Koinotarxis
                {
                    FullName = "Test Name1", Tel = "1256789" , Sex = Sex.Female, Thesi = Thesi.Koinotarxis, Age = 30
                }
            },
            new Koinotita
            {
                Id = 2, Name = "TestKoinotita2", Koinotarxis = new Koinotarxis()
                {
                    FullName = "Test Name2" , Tel = "23457890" , Sex = Sex.Female, Thesi = Thesi.Koinotarxis, Age = 30
                }
            }
        };
        var tomeas = new Tomeas { Id = 1, Name = "TestTomeas", Koinotites = new List<Koinotita>() };
        tomeas.Koinotites = teams;

        _mockdteamsRepository.Setup(m => m.GetKoinotitesAnaTomeaInDb(new(),tomeasId)).ReturnsAsync(teams);

        // Act
        var result = await _teamsService.GetAllKoinotitesInService(new());

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateSkiniInService_ShouldReturnTeam()
    {
        // Arrange
        var team = new SkiniDto { Id = 1, Name = "TestTeam" };
        _mockdteamsRepository.Setup(m => m.UpdateSkiniInDb(It.IsAny<Skini>())).ReturnsAsync(true);

        // Act
        var result = await _teamsService.UpdateSkiniInService(team);

        // Assert
        Assert.True(result);
    }
}
