using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using StelexarasApp.Library.QueryParameters.People;

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
              LastName = dto.LastName,
              FirstName = dto.FirstName,
              Sex = dto.Sex,
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
        var paidiDto = new CreatePaidiRequest { LastName = "Doe", FirstName = "John", Age = 16, PaidiType = PaidiType.Ekpaideuomenos, SkiniName = "Skini1" };
        var paidi = new Paidi { Id = 1, LastName = "John", FirstName = "John", Age = 16, PaidiType = PaidiType.Ekpaideuomenos };

        _mockMapper.Setup(m => m.Map<Paidi>(paidiDto)).Returns(paidi);
        _mockPaidiRepository.Setup(repo => repo.AddPaidiInSkini(paidi, "Skini1")).ReturnsAsync(true);
        _paidiValidatorMock.Setup(v => v.Validate(paidiDto)).Returns(new FluentValidation.Results.ValidationResult());

        // Act
        var result = await _paidiService.CreatePaidiInService(paidiDto);

        // Assert
        Assert.True(result.IsSuccess);
        _mockPaidiRepository.Verify(repo => repo.AddPaidiInSkini(paidi, "Skini1"), Times.Once);
    }

    [Fact]
    public async Task GetEkpaideuomenous_ShouldReturnThem()
    {
        // Arrange
        var expectedPaidia = new List<Paidi>
        {
            new Paidi { Id = 1, LastName = "Doe", FirstName = "Georg", Sex = Sex.Male, Age = 16, PaidiType = PaidiType.Ekpaideuomenos },
            new Paidi { Id = 2, LastName = "Smith", FirstName = "Georg",Sex = Sex.Female, Age = 16, PaidiType = PaidiType.Ekpaideuomenos }
        }.Where(p => p.PaidiType == PaidiType.Ekpaideuomenos).ToList();

        _mockPaidiRepository
            .Setup(repo => repo.GetPaidiaInSxoliFromDb(new PaidiQueryParameters()))
            .ReturnsAsync(expectedPaidia);

        _mockMapper.Setup(m => m.Map<IEnumerable<PaidiResponse>>(It.IsAny<IEnumerable<Paidi>>()))
            .Returns((IEnumerable<Paidi> paidia) => paidia.Select(p => new PaidiResponse
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Sex = Sex.Female,
                Age = p.Age,
                PaidiType = p.PaidiType
            }));

        // Act
        var result = await _paidiService.GetPaidiaBySxoliInService(new PaidiQueryParameters());

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
            LastName = "Doe",
            FirstName = "John",
            Sex = Sex.Female,
            Age = 30,
            PaidiType = PaidiType.Ekpaideuomenos
        };

        _mockPaidiRepository.Setup(repo => repo.GetPaidiByIdFromDb(paidiToDelete.Id, new PaidiQueryParameters())).ReturnsAsync(paidiToDelete);
        _mockPaidiRepository.Setup(repo => repo.DeletePaidiInDb(paidiToDelete.Id)).ReturnsAsync(true);

        var result = await _paidiService.DeletePaidiInService(paidiToDelete.Id);

        Assert.True(result.IsSuccess);
        _mockPaidiRepository.Verify(repo => repo.GetPaidiByIdFromDb(paidiToDelete.Id, new PaidiQueryParameters()), Times.Once);
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
            LastName = expectedFullName.Split(' ')[0],
            FirstName = expectedFullName.Split(' ')[1],
            Age = expectedAge,
            PaidiType = expectedType
        } : null;

        _mockPaidiRepository.Setup(repo => repo.GetPaidiByIdFromDb(paidiId, new PaidiQueryParameters()))
                            .ReturnsAsync(expectedPaidi);
        _mockMapper.Setup(m => m.Map<PaidiResponse>(It.IsAny<Paidi>()))
                   .Returns((Paidi p) => new PaidiResponse
                   {
                       Id = p.Id,
                       LastName = p.LastName,
                       FirstName = p.FirstName,
                       Sex = p.Sex,
                       Age = p.Age,
                       PaidiType = p.PaidiType
                   });

        // Act
        var result = await _paidiService.GetPaidiByIdInService(paidiId, new PaidiQueryParameters());

        // Assert
        if (shouldExist)
        {
            Assert.NotNull(result);
            Assert.Equal(expectedPaidi?.Id, result.Value.Id);
            Assert.Equal(expectedPaidi?.LastName, result.Value.LastName);
            Assert.Equal(expectedPaidi?.FirstName, result.Value.FirstName);
            Assert.Equal(expectedPaidi?.Sex, result.Value.Sex);
            Assert.Equal(expectedPaidi?.Age, result.Value.Age);
            Assert.Equal(expectedPaidi?.PaidiType, result.Value.PaidiType);
        }
        else
        {
            Assert.Null(result);
        }
    }
}
