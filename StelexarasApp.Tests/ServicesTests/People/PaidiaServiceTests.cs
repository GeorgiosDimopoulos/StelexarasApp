using AutoMapper;
using Moq;
using StelexarasApp.DataAccess;
using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using FluentValidation;
using StelexarasApp.Library.Models.Atoma.Children;

namespace StelexarasApp.Tests.ServicesTests.People;

public class PaidiaServiceTests
{
    private readonly Mock<IPaidiRepository> _mockPaidiRepository;
    private readonly EkpaideuomenosService ekpaideuomenosService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<AppDbContext> _mockDbContext;
    private readonly Mock<ILoggerFactory> _loggerFactory;
    private readonly Mock<IValidator<EkpaideuomenosDtoBase>> _ekpaideuomenosValidatorMock;
    private readonly Mock<IValidator<KataskinotisDtoBase>> _kataskinotisValidatorMock;

    public PaidiaServiceTests()
    {
        _mockDbContext = new Mock<AppDbContext>();
        _loggerFactory = new Mock<ILoggerFactory>();
        _mockPaidiRepository = new Mock<IPaidiRepository>();
        _mockMapper = new Mock<IMapper>();
        _ekpaideuomenosValidatorMock = new Mock<IValidator<EkpaideuomenosDtoBase>>();

        _mockMapper.Setup(m => m.Map<Ekpaideuomenos>(It.IsAny<EkpaideuomenosDtoBase>()))
          .Returns((EkpaideuomenosDtoBase dto) => new Ekpaideuomenos
          {
              FullName = dto.FullName,
              Age = dto.Age,
              PaidiType = dto.PaidiType
          });

        ekpaideuomenosService = new EkpaideuomenosService(
            _mockPaidiRepository.Object,
            _mockMapper.Object,
            _loggerFactory.Object.CreateLogger<EkpaideuomenosService>(),
            _ekpaideuomenosValidatorMock.Object);

    }

    [Fact]
    public async Task AddEkpaideuomenos_ShouldReturnTrue_WhenSuccessful()
    {
        // Arrange
        var paidiDto = new CreateEkpaideuomenosRequest { FullName = "John Doe", Age = 16, PaidiType = PaidiType.Ekpaideuomenos, SkiniName = "Skini1" };
        var paidi = new Paidi { Id = 1, FullName = "John Doe", Age = 16, PaidiType = PaidiType.Ekpaideuomenos};

        _mockMapper.Setup(m => m.Map<Paidi>(paidiDto)).Returns(paidi);
        _mockPaidiRepository.Setup(repo => repo.AddPaidiInDb(paidi)).ReturnsAsync(true);
        _ekpaideuomenosValidatorMock.Setup(v => v.Validate(paidiDto)).Returns(new FluentValidation.Results.ValidationResult());

        // Act
        var result = await ekpaideuomenosService.CreateEkpaideuomenosInService(paidiDto);

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
        var result = await ekpaideuomenosService.GetEkpaideuomenoiInService();

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
        var paidiToDeleteRequest = new DeleteEkpaideuomenosRequest
        {
            Id = 1
        };

        var paidiToDelete = new Paidi
        {
            Id = 1,
            FullName = "John Doe",
            Age = 30,
            PaidiType = PaidiType.Ekpaideuomenos
        };

        _mockPaidiRepository.Setup(repo => repo.GetPaidiByIdFromDb(paidiToDelete.Id)).ReturnsAsync(paidiToDelete);
        _mockPaidiRepository.Setup(repo => repo.DeletePaidiInDb(paidiToDelete)).ReturnsAsync(true);

        var result = await ekpaideuomenosService.DeleteEkpaideuomenosInService(paidiToDeleteRequest);

        Assert.True(result);
        _mockPaidiRepository.Verify(repo => repo.GetPaidiByIdFromDb(paidiToDelete.Id), Times.Once);
        _mockPaidiRepository.Verify(repo => repo.DeletePaidiInDb(paidiToDelete), Times.Once);
    }

    [Theory]
    [InlineData(1, "John Doe", 30, PaidiType.Kataskinotis, true)]
    public async Task GetPaidiById_ShouldReturnExpectedResult(int paidiId, string expectedFullName, int expectedAge, PaidiType expectedType, bool shouldExist)
    {
        // Arrange
        var expectedPaidi = shouldExist ? new Paidi
        {
            Id = paidiId,
            FullName = expectedFullName,
            Age = expectedAge,
            PaidiType = expectedType
        } : null;

        _mockPaidiRepository.Setup(repo => repo.GetPaidiByIdFromDb(paidiId))
            .ReturnsAsync(expectedPaidi);

        // Act
        var result = await ekpaideuomenosService.GetEkpaideuomenosByIdInService(paidiId);

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
