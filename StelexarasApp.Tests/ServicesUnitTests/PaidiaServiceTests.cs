using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using StelexarasApp.Library.QueryParameters.People;

namespace StelexarasApp.Tests.ServicesUnitTests;

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

    #region Create    
    [Fact]
    public async Task AddEkpaideuomenos_ShouldReturnTrue_WhenSuccessful()
    {
        // Arrange
        var paidiDto = new CreatePaidiRequest { LastName = "Doe", FirstName = "John", Age = 16, PaidiType = PaidiType.Ekpaideuomenos, SkiniName = "Skini1", ParentTel  = "1234567290", SeAdeia = false, Sex = Sex.Male };
        var paidi = new Ekpaideuomenos { Id = 1, LastName = "Doe", FirstName = "John", Age = 16, PaidiType = PaidiType.Ekpaideuomenos, Sex = Sex.Male, SeAdeia = false, ParentTel = "1234567290", SkiniId = 1 };

        _mockMapper.Setup(m => m.Map<Paidi>(paidiDto)).Returns(paidi);
        _mockPaidiRepository.Setup(repo => repo.AddPaidiInSkini(paidi, "Skini1")).ReturnsAsync(true);
        _paidiValidatorMock.Setup(v => v.ValidateAsync(paidiDto, It.IsAny<CancellationToken>())).ReturnsAsync(new FluentValidation.Results.ValidationResult());

        // Act
        var result = await _paidiService.CreatePaidiInService(paidiDto);

        // Assert
        Assert.True(result.IsSuccess);
        _mockPaidiRepository.Verify(repo => repo.AddPaidiInSkini(paidi, "Skini1"), Times.Once);
    }

    [Fact]
    public async Task CreatePaidi_ShouldFail_WhenValidationFails()
    {
        // Arrange
        var newPaidiRequest = new CreatePaidiRequest { LastName = string.Empty, FirstName = "John", Age = 16, PaidiType = PaidiType.Ekpaideuomenos, SkiniName = "Skini1", ParentTel  = "1234567890", SeAdeia = false, Sex = Sex.Male };

        _paidiValidatorMock.Setup(v => v.ValidateAsync(newPaidiRequest, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult(
            [new ValidationFailure("LastName", "PaidiDto last Name is required")]));

        // Act
        var result = await _paidiService.CreatePaidiInService(newPaidiRequest);

        // Assert
        Assert.True(result.IsFailed);
        _mockPaidiRepository.Verify(repo => repo.AddPaidiInSkini(It.IsAny<Paidi>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task CreatePaidi_ShouldFail_WhenRepositoryReturnsFalse()
    {
        // Arrange
        var newPaidiRequest = new CreatePaidiRequest
        {
            LastName = "Dae",
            FirstName = "John",
            Age = 16,
            PaidiType = PaidiType.Ekpaideuomenos,
            ParentTel  = "1234567890",
            SeAdeia = false,
            Sex = Sex.Male,
            SkiniName = "Skini1",
        };

        _paidiValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<CreatePaidiRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());
        _mockPaidiRepository.Setup(repo => repo.AddPaidiInSkini(It.IsAny<Paidi>(), It.IsAny<string>())).ReturnsAsync(false);

        // Act
        var result = await _paidiService.CreatePaidiInService(newPaidiRequest);

        // Assert
        Assert.True(result.IsFailed);
        _mockPaidiRepository.Verify(repo => repo.AddPaidiInSkini(It.IsAny<Paidi>(), It.IsAny<string>()), Times.Once);
    }
    #endregion

    #region Get
    [Fact]
    public async Task GetEkpaideuomenous_ShouldReturnThem()
    {
        // Arrange
        var expectedPaidia = new List<Paidi>
        {
            new Ekpaideuomenos { Id = 1, LastName = "Doe", FirstName = "Georg", Sex = Sex.Male, Age = 16, PaidiType = PaidiType.Ekpaideuomenos, ParentTel = "1234567890", SeAdeia = false, SkiniId = 1 },
            new Ekpaideuomenos { Id = 2, LastName = "Smith", FirstName = "Georg",Sex = Sex.Female, Age = 16, PaidiType = PaidiType.Ekpaideuomenos, ParentTel = "0987654321", SeAdeia = true, SkiniId = 2 }
        };

        _mockPaidiRepository
            .Setup(repo => repo.GetPaidiaInSxoliFromDb(It.IsAny<PaidiQueryParameters>()))
            .ReturnsAsync(expectedPaidia);

        _mockMapper.Setup(m => m.Map<IEnumerable<PaidiResponse>>(It.IsAny<IEnumerable<Paidi>>()))
                   .Returns((IEnumerable<Paidi> paidia) => paidia.Select(p => new PaidiResponse
                   {
                       Id = p.Id,
                       FirstName = p.FirstName,
                       LastName = p.LastName,
                       SeAdeia = p.SeAdeia,
                       ParentTel  = p.ParentTel,
                       SkiniName = p.Skini?.Name,
                       Sex = p.Sex,
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
    public async Task GetPaidiById_ShouldReturn()
    {
        // Arrange
        var expectedPaidi = new Paidi
        {
            Id = 192,
            LastName = "Doe",
            FirstName = "John",
            Age = 30,
            PaidiType = PaidiType.Kataskinotis,
        };

        _mockPaidiRepository.Setup(repo => repo.GetPaidiByIdFromDb(expectedPaidi.Id, It.IsAny<PaidiQueryParameters>()))
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
        var result = await _paidiService.GetPaidiByIdInService(expectedPaidi.Id, new PaidiQueryParameters());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedPaidi?.Id, result.Value.Id);
        Assert.Equal(expectedPaidi?.LastName, result.Value.LastName);
        Assert.Equal(expectedPaidi?.FirstName, result.Value.FirstName);
        Assert.Equal(expectedPaidi?.Sex, result.Value.Sex);
        Assert.Equal(expectedPaidi?.Age, result.Value.Age);
        Assert.Equal(expectedPaidi?.PaidiType, result.Value.PaidiType);
    }

    [Fact]
    public async Task GetPaidiById_ShouldFail_WhenNotFound()
    {
        // Arrange
        int paidiId = 129;

        _mockPaidiRepository.Setup(repo => repo.GetPaidiByIdFromDb(paidiId, It.IsAny<PaidiQueryParameters>()))
                            .ReturnsAsync((Paidi?)null);

        // Act
        var result = await _paidiService.GetPaidiByIdInService(paidiId, new PaidiQueryParameters());

        // Assert
        Assert.True(result.IsFailed);
        _mockPaidiRepository.Verify(repo => repo.GetPaidiByIdFromDb(paidiId, It.IsAny<PaidiQueryParameters>()), Times.Once);
    }
    #endregion

    #region Update
    [Fact]
    public async Task UpdatePaidi_ShouldFail_WhenRepositoryReturnsFalse()
    {
        // Arrange
        var paidiRequest =
            new UpdatePaidiRequest
            {
                Id = 123,
                LastName = "UpdatedLastName",
                FirstName = "UpdatedFirstName",
                Age = 10,
                SeAdeia = true,
                Sex = Sex.Male,
                PaidiType = PaidiType.Ekpaideuomenos,
                ParentTel  = "1234567890",
                SkiniName = "Skini1"
            };

        var mappedPaidi = new Paidi
        {
            Id = paidiRequest.Id,
            FirstName = paidiRequest.FirstName,
            LastName = paidiRequest.LastName,
            SeAdeia = paidiRequest.SeAdeia,
            Age = paidiRequest.Age,
            PaidiType = paidiRequest.PaidiType,
            ParentTel = paidiRequest.ParentTel ,
            Sex = paidiRequest.Sex
        };

        _paidiValidatorMock.Setup(v => v.ValidateAsync(paidiRequest, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(new ValidationResult());
        _mockMapper.Setup(m => m.Map<Paidi>(paidiRequest))
                   .Returns(mappedPaidi);
        _mockPaidiRepository.Setup(repo => repo.UpdatePaidiInDb(mappedPaidi))
                            .ReturnsAsync(false);

        // Act
        var result = await _paidiService.UpdatePaidiInService(paidiRequest);

        // Assert
        Assert.True(result.IsFailed);
        _mockPaidiRepository.Verify(repo => repo.UpdatePaidiInDb(mappedPaidi), Times.Once);
    }

    [Fact]
    public async Task UpdatePaidi_ShouldFail_WhenValidationFails()
    {
        var updatePaidiRequest = new UpdatePaidiRequest()
        {
            Id = 124,
            LastName = "UpdateddLastName",
            FirstName = "UpdateddFirstName",
            Age = 10,
            SeAdeia = true,
            Sex = Sex.Male,
            PaidiType = PaidiType.Ekpaideuomenos,
            ParentTel  = "1234567290",
            SkiniName = "Skini11"
        };

        _paidiValidatorMock.Setup(v => v.ValidateAsync(updatePaidiRequest, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(new ValidationResult([new ValidationFailure("LastName", "LastName is required")]));


        // Act
        var result = await _paidiService.UpdatePaidiInService(updatePaidiRequest);

        // Assert
        Assert.True(result.IsFailed);
        _mockPaidiRepository.Verify(repo => repo.UpdatePaidiInDb(It.IsAny<Paidi>()), Times.Never);
    }
    #endregion

    #region Delete
    [Fact]
    public async Task DeletePaidiShouldReturnOk()
    {
        var paidiToDelete = new Paidi
        {
            Id = 1,
            LastName = "Deoe",
            FirstName = "Joohn",
            ParentTel = "1233567890",
            SeAdeia = false,
            SkiniId = 1,
            Sex = Sex.Female,
            Age = 30,
            PaidiType = PaidiType.Ekpaideuomenos
        };

        _mockPaidiRepository.Setup(repo => repo.GetPaidiByIdFromDb(paidiToDelete.Id, It.IsAny<PaidiQueryParameters>())).ReturnsAsync(paidiToDelete);
        _mockPaidiRepository.Setup(repo => repo.DeletePaidiInDb(paidiToDelete.Id)).ReturnsAsync(true);

        var result = await _paidiService.DeletePaidiInService(paidiToDelete.Id);

        Assert.True(result.IsSuccess);
        _mockPaidiRepository.Verify(repo => repo.GetPaidiByIdFromDb(paidiToDelete.Id, It.IsAny<PaidiQueryParameters>()), Times.Once);
        _mockPaidiRepository.Verify(repo => repo.DeletePaidiInDb(paidiToDelete.Id), Times.Once);
    }

    [Fact]
    public async Task DeletePaidi_ShouldFail_WhenNotFound()
    {
        var paidiToDelete = new Paidi
        {
            Id = 1,
            LastName = "Deoe",
            FirstName = "Joohn",
            ParentTel = "1233567890",
            SeAdeia = false,
            SkiniId = 1,
            Sex = Sex.Female,
            Age = 30,
            PaidiType = PaidiType.Ekpaideuomenos
        };

        _mockPaidiRepository.Setup(repo => repo.GetPaidiByIdFromDb(paidiToDelete.Id, It.IsAny<PaidiQueryParameters>())).ReturnsAsync((Paidi?)null);
        _mockPaidiRepository.Setup(repo => repo.DeletePaidiInDb(paidiToDelete.Id)).ReturnsAsync(false);

        var result = await _paidiService.DeletePaidiInService(paidiToDelete.Id);

        Assert.True(result.IsFailed);
        _mockPaidiRepository.Verify(repo => repo.GetPaidiByIdFromDb(paidiToDelete.Id, It.IsAny<PaidiQueryParameters>()), Times.Once);
        _mockPaidiRepository.Verify(repo => repo.DeletePaidiInDb(paidiToDelete.Id), Times.Never);
    }

    [Fact]
    public async Task DeletePaidi_ShouldFail_WhenRepositoryReturnsFalse()
    {
        var paidiToDelete = new Paidi
        {
            Id = 1,
            LastName = "Deoe",
            FirstName = "Joohn",
            ParentTel = "1233567890",
            SeAdeia = false,
            SkiniId = 1,
            Sex = Sex.Female,
            Age = 30,
            PaidiType = PaidiType.Ekpaideuomenos
        };

        _mockPaidiRepository.Setup(repo => repo.GetPaidiByIdFromDb(paidiToDelete.Id, It.IsAny<PaidiQueryParameters>())).ReturnsAsync(paidiToDelete);
        _mockPaidiRepository.Setup(repo => repo.DeletePaidiInDb(paidiToDelete.Id)).ReturnsAsync(false);

        var result = await _paidiService.DeletePaidiInService(paidiToDelete.Id);

        Assert.True(result.IsFailed);
        _mockPaidiRepository.Verify(repo => repo.GetPaidiByIdFromDb(paidiToDelete.Id, It.IsAny<PaidiQueryParameters>()), Times.Once);
        _mockPaidiRepository.Verify(repo => repo.DeletePaidiInDb(paidiToDelete.Id), Times.Once);
    }
    #endregion
}
