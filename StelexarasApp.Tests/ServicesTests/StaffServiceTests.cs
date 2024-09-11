using AutoMapper;
using Microsoft.Maui.Platform;
using Moq;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.Services;

namespace StelexarasApp.Tests.ServicesTests;

public class StaffServiceTests
{
    private readonly Mock<IStaffRepository> _mockStelexiRepository;
    private readonly StaffService _stelexiService;
    private readonly Mock<IMapper> _mockMapper;
    public StaffServiceTests()
    {
        _mockStelexiRepository = new Mock<IStaffRepository>();
        _mockMapper = new Mock<IMapper>();
        _stelexiService = new StaffService(_mockMapper.Object, _mockStelexiRepository.Object);
    }

    [Fact]
    public async Task GetStelexosByIdInService_ShouldReturnStelexos_WhenRepositoryReturnsStelexos()
    {
        // Arrange
        var id = 1;
        var thesi = Thesi.Omadarxis;
        var stelexos = new Omadarxis
        {
            Id = id,
            Thesi = thesi,
            FullName = "Test Name"
        };

        _mockStelexiRepository.Setup(r => r.FindStelexosByIdInDb(id, thesi)).ReturnsAsync(stelexos);

        // Act
        var result = await _stelexiService.GetStelexosByIdInService(id, thesi);

        // Assert
        Assert.Equal(stelexos, result);
        _mockStelexiRepository.Verify(r => r.FindStelexosByIdInDb(id, thesi), Times.Once);
    }


    [Fact]
    public async Task GetStelexoiAnaThesi_ShouldReturnStelexoi()
    {
        // Arrange
        var stelexoi = new List<Omadarxis>
        {
            new Omadarxis { Id = 1, FullName = "John Doe", Age = 30, Thesi = Thesi.Omadarxis }
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaThesiFromDb(Thesi.Omadarxis)).ReturnsAsync(stelexoi);

        // Act
        var result = await _stelexiService.GetStelexoiAnaThesiInService(Thesi.Omadarxis);
        
        // Assert
        Assert.Single(result);
        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaThesiFromDb(Thesi.Omadarxis), Times.Once);
    }

    [Theory]
    [InlineData("Test Name", "TestXwros", Thesi.Omadarxis, 2)]
    public async Task AddOmadarxisInService_ShouldReturnExpectedResult(
        string fullName, string xwrosName, Thesi thesi, int id)
    {
        // Arrange
        var stelexosDto = new OmadarxisDto
        {
            FullName = fullName,
            Id = id,
            Age = 30,
            Sex = Sex.Male,
            Thesi = thesi
        };
        var stelexos = new Omadarxis
        {
            FullName = fullName,
            Id = id,
            Age = 30,
            Sex = Sex.Male,
            Thesi = thesi
        };

        _mockMapper.Setup(m => m.Map<Stelexos>(stelexosDto)).Returns(stelexos);
        _mockStelexiRepository.Setup(repo => repo.AddStelexosInDb(stelexos)).ReturnsAsync(true);

        // Act
        var result = await _stelexiService.AddStelexosInService(stelexosDto, stelexosDto.Thesi);

        // Assert
        Assert.True(result);
        _mockStelexiRepository.Verify(r => r.AddStelexosInDb(stelexos), Times.Once);
    }

    [Fact]
    public async Task DeleteStelexosInService_ShouldReturnTrue_WhenRepositoryReturnsTrue()
    {
        // Arrange
        var id = 1;
        var thesi = new Thesi {};
        _mockStelexiRepository.Setup(r => r.DeleteStelexosInDb(id, thesi)).ReturnsAsync(true);

        // Act
        var result = await _stelexiService.DeleteStelexosInService(id, thesi);

        // Assert
        Assert.True(result);
        _mockStelexiRepository.Verify(r => r.DeleteStelexosInDb(id, thesi), Times.Once);
    }


}
