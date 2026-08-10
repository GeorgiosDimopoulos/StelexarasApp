using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;

namespace StelexarasApp.Tests.ServicesTests;

public class PaidiaServiceTests
{
    private readonly Mock<IPaidiaRepository> _mockPaidiRepository;
    private readonly PaidiaService _paidiService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILoggerFactory> _loggerFactory;
    private readonly Mock<IValidator<IPaidiDto>> _paidiValidatorMock;

    public PaidiaServiceTests()
    {
        _loggerFactory = new Mock<ILoggerFactory>();
        _mockPaidiRepository = new Mock<IPaidiaRepository>();
        _mockMapper = new Mock<IMapper>();
        _paidiValidatorMock = new Mock<IValidator<IPaidiDto>>();
        _paidiService = new PaidiaService(_mockPaidiRepository.Object, _mockMapper.Object, _loggerFactory.Object.CreateLogger<PaidiaService>(), _paidiValidatorMock.Object);

        _mockMapper.Setup(m => m.Map<Ekpaideuomenos>(It.IsAny<IPaidiDto>()))
          .Returns((IPaidiDto dto) => new Ekpaideuomenos
          {
              FullName = dto.FullName,
              Age = dto.Age,
              PaidiType = dto.PaidiType
          });

        _paidiService = new PaidiaService(
            _mockPaidiRepository.Object,
            _mockMapper.Object,
            _loggerFactory.Object.CreateLogger<PaidiaService>(),
            _paidiValidatorMock.Object);

    }

    [Fact]
    public async Task AddEkpaideuomenos_ShouldReturnTrue_WhenSuccessful()
    {
        // Arrange
        var paidiDto = new CreatePaidiRequest { FullName = "John Doe", Age = 16, PaidiType = PaidiType.Ekpaideuomenos, SkiniName = "Skini1" };
        var paidi = new Paidi { Id = 1, FullName = "John Doe", Age = 16, PaidiType = PaidiType.Ekpaideuomenos };

        _mockMapper.Setup(m => m.Map<Paidi>(paidiDto)).Returns(paidi);
        _mockPaidiRepository.Setup(repo => repo.AddPaidiInDb(paidi)).ReturnsAsync(true);
        _paidiValidatorMock.Setup(v => v.Validate(paidiDto)).Returns(new FluentValidation.Results.ValidationResult());

        // Act
        var result = await _paidiService.CreatePaidiInService(paidiDto);

        // Assert
        Assert.True(result);
        _mockPaidiRepository.Verify(repo => repo.AddPaidiInDb(paidi), Times.Once);
    }

    [Fact]
    public async Task GetEkpaideuomenous_ShouldReturnThem()
    {
        // Arrange
        var expectedPaidia = new List<Paidi>
        {
            new Paidi { Id = 1, FullName = "John Doe", Age = 16, PaidiType = PaidiType.Ekpaideuomenos },
            new Paidi { Id = 2, FullName = "Jane Smith", Age = 16, PaidiType = PaidiType.Ekpaideuomenos }
        }.Where(p => p.PaidiType == PaidiType.Ekpaideuomenos).ToList();

        _mockPaidiRepository
            .Setup(repo => repo.GetPaidiaInSxoliFromDb())
            .ReturnsAsync(expectedPaidia);

        _mockMapper.Setup(m => m.Map<IEnumerable<PaidiResponse>>(It.IsAny<IEnumerable<Paidi>>()))
            .Returns((IEnumerable<Paidi> paidia) => paidia.Select(p => new PaidiResponse
            {
                Id = p.Id,
                FullName = p.FullName,
                Age = p.Age,
                PaidiType = p.PaidiType
            }));

        // Act
        var result = await _paidiService.GetPaidiaBySxoliInService();

        // Assert
        Assert.Equal(2, result.Count());
        foreach (var paidi in result)
        {
            Assert.Equal(PaidiType.Ekpaideuomenos, paidi.PaidiType);
        }
    }

    [Fact]
    public async Task DeletePaidiShouldReturnOk()
    {
        var paidiToDelete = new Paidi
        {
            Id = 1,
            FullName = "John Doe",
            Age = 30,
            PaidiType = PaidiType.Ekpaideuomenos
        };

        _mockPaidiRepository.Setup(repo => repo.GetPaidiByIdFromDb(paidiToDelete.Id)).ReturnsAsync(paidiToDelete);
        _mockPaidiRepository.Setup(repo => repo.DeletePaidiInDb(paidiToDelete.Id)).ReturnsAsync(true);

        var result = await _paidiService.DeletePaidiInService(paidiToDelete.Id);

        Assert.True(result);
        _mockPaidiRepository.Verify(repo => repo.GetPaidiByIdFromDb(paidiToDelete.Id), Times.Once);
        _mockPaidiRepository.Verify(repo => repo.DeletePaidiInDb(paidiToDelete.Id), Times.Once);
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
        _mockMapper.Setup(m => m.Map<PaidiResponse>(It.IsAny<Paidi>()))
                   .Returns((Paidi p) => new PaidiResponse
                   {
                       Id = p.Id,
                       FullName = p.FullName,
                       Age = p.Age,
                       PaidiType = p.PaidiType
                   });

        // Act
        var result = await _paidiService.GetPaidiByIdInService(paidiId);

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
