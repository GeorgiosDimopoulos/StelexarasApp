using AutoMapper;
using Moq;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.Services;
using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.Tests.ServicesTests;

public class PaidiaServiceTests
{
    private readonly Mock<IPaidiRepository> _mockPaidiRepository;
    private readonly PaidiaService _paidiaService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<AppDbContext> _mockDbContext;
    private readonly Mock<ILoggerFactory> _loggerFactory;
    public PaidiaServiceTests()
    {
        _mockDbContext = new Mock<AppDbContext>();
        _loggerFactory = new Mock<ILoggerFactory>();

        // _mockPaidiRepository = new Mock<PaidiRepository>(_mockDbContext.Object, _loggerFactory.Object);
        _mockPaidiRepository = new Mock<IPaidiRepository>();
        _mockMapper = new Mock<IMapper>();

        _mockMapper.Setup(m => m.Map<Paidi>(It.IsAny<PaidiDto>()))
          .Returns((PaidiDto dto) => new Paidi
          {
              Id = dto.Id,
              FullName = dto.FullName,
              Age = dto.Age,
              PaidiType = dto.PaidiType
          });
        _paidiaService = new PaidiaService(_mockPaidiRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task AddEkpaideuomenos_ShouldReturnTrue_WhenSuccessful()
    {
        // Arrange
        var paidiDto = new PaidiDto { Id = 1, FullName = "John Doe", Age = 30, PaidiType = PaidiType.Ekpaideuomenos };
        var paidi = new Paidi { Id = 1, FullName = "John Doe", Age = 30, PaidiType = PaidiType.Ekpaideuomenos };

        _mockMapper.Setup(m => m.Map<Paidi>(paidiDto)).Returns(paidi);
        _mockPaidiRepository.Setup(repo => repo.AddPaidiInDb(paidi)).ReturnsAsync(true);

        // Act
        var result = await _paidiaService.AddPaidiInService(paidiDto);

        // Assert
        Assert.True(result);
        _mockPaidiRepository.Verify(repo => repo.AddPaidiInDb(paidi), Times.Once);
    }

    [Theory]
    [InlineData(PaidiType.Kataskinotis, 2)]
    [InlineData(PaidiType.Ekpaideuomenos, 2)]
    public async Task GetPaidia_ShouldReturnPaidia(PaidiType paidiType, int expectedCount)
    {
        // Arrange
        var expectedPaidia = new List<Paidi>
        {
            new Paidi { Id = 1, FullName = "John Doe", Age = 30, PaidiType = paidiType },
            new Paidi { Id = 2, FullName = "Jane Smith", Age = 25, PaidiType = paidiType }
        }.Where(p => p.PaidiType == paidiType).ToList();

        _mockPaidiRepository
            .Setup(repo => repo.GetPaidiaFromDb(paidiType))
            .ReturnsAsync(expectedPaidia);

        // Act
        var result = await _paidiaService.GetPaidiaInService(paidiType);

        // Assert
        Assert.Equal(expectedCount, result.Count());
        foreach (var paidi in result)
        {
            Assert.Equal(paidiType, paidi.PaidiType);
        }
    }

    [Fact]
    public async Task DeletePaidiShouldReturnOk()
    {
        var paidiId = 1;
        var paidi = new Paidi
        {
            Id = paidiId,
            FullName = "Test Name",
            PaidiType = PaidiType.Ekpaideuomenos
        };

        _mockPaidiRepository.Setup(repo => repo.GetPaidiByIdFromDb(paidiId)).ReturnsAsync(paidi);
        _mockPaidiRepository.Setup(repo => repo.DeletePaidiInDb(paidi)).ReturnsAsync(true);

        var result = await _paidiaService.DeletePaidiInService(paidiId);

        Assert.True(result);
        _mockPaidiRepository.Verify(repo => repo.GetPaidiByIdFromDb(paidiId), Times.Once);
        _mockPaidiRepository.Verify(repo => repo.DeletePaidiInDb(paidi), Times.Once);
    }

    [Theory]
    [InlineData(1, "John Doe", 30, PaidiType.Kataskinotis, true)]
    [InlineData(2, null, 0, PaidiType.Unknown, false)]
    public async Task GetPaidiById_ShouldReturnExpectedResult(int paidiId, string expectedFullName, int expectedAge, PaidiType expectedType, bool shouldExist)
    {
        // Arrange
        Paidi? expectedPaidi = shouldExist ? new Paidi
        {
            Id = paidiId,
            FullName = expectedFullName,
            Age = expectedAge,
            PaidiType = expectedType
        } : null;

        _mockPaidiRepository.Setup(repo => repo.GetPaidiByIdFromDb(paidiId))
            .ReturnsAsync(expectedPaidi);

        // Act
        var result = await _paidiaService.GetPaidiByIdInService(paidiId);

        // Assert
        if (shouldExist)
        {
            Assert.NotNull(result);
            Assert.Equal(expectedPaidi?.Id, result.Id);
            Assert.Equal(expectedPaidi?.FullName, result.FullName);
            Assert.Equal(expectedPaidi?.Age, result.Age);
            Assert.Equal(expectedPaidi?.PaidiType, result.PaidiType);
        }
        else
        {
            Assert.Null(result);
        }
    }
}
