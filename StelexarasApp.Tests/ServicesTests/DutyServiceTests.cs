using Moq;
using StelexarasApp.Library.Models;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.Services;

namespace StelexarasApp.Tests.ServicesTests;

public class DutyServiceTests
{
    private readonly Mock<IDutyRepository> _mockdutyRepository;
    private readonly DutyService _dutyService;

    public DutyServiceTests()
    {
        _mockdutyRepository = new Mock<IDutyRepository>();
        _dutyService = new DutyService(_mockdutyRepository.Object);
    }

    [Fact]
    public async Task AddDutyInService_ShouldReturnTrue()
    {
        // Arrange
        var duty = new Duty { Id = 1, Name = "TestDuty", Date = DateTime.Now };
        _mockdutyRepository.Setup(m => m.AddDutyInDb(It.IsAny<Duty>())).ReturnsAsync(true);

        // Act
        var result = await _dutyService.AddDutyInService(duty);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteDutyInService_ShouldReturnTrue()
    {
        // Arrange
        var duty = new Duty { Id = 1, Name = "TestDuty", Date = DateTime.Now };
        _mockdutyRepository.Setup(m => m.DeleteDutyInDb(It.IsAny<int>())).ReturnsAsync(true);

        // Act
        var result = await _dutyService.DeleteDutyInService(duty.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetDutiesInService_ShouldReturnDuties()
    {
        // Arrange
        var duties = new List<Duty>
        {
            new Duty { Id = 1, Name = "TestDuty1", Date = DateTime.Now },
            new Duty { Id = 2, Name = "TestDuty2", Date = DateTime.Now }
        };
        _mockdutyRepository.Setup(m => m.GetDutiesFromDb()).ReturnsAsync(duties);

        // Act
        var result = await _dutyService.GetDutiesInService();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task UpdateDutyByIdInService()
    {
        // Arrange
        var duty = new Duty { Id = 1, Name = "TestDuty", Date = DateTime.Now };
        _mockdutyRepository.Setup(m => m.UpdateDutyInDb(It.IsAny<string>(), It.IsAny<Duty>())).ReturnsAsync(true);

        // Act
        var result = await _dutyService.UpdateDutyInService(duty.Name, duty);

        // Assert
        Assert.True(result);
    }
}
