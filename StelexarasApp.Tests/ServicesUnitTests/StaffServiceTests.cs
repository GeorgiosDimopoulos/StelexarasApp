using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using StelexarasApp.Library.QueryParameters.People;

namespace StelexarasApp.Tests.ServicesUnitTests;

public class StaffServiceTests
{
    private readonly Mock<IStaffRepository> _mockStelexiRepository;
    private readonly IStaffService _stelexiService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IValidator<StelexosDtoBase>> _stelexosValidator;

    public StaffServiceTests()
    {
        _stelexosValidator = new Mock<IValidator<StelexosDtoBase>>();
        _mockStelexiRepository = new Mock<IStaffRepository>();
        _mockMapper = new Mock<IMapper>();
        _stelexiService = new StaffService(_mockMapper.Object, _mockStelexiRepository.Object, _stelexosValidator.Object);
    }

    #region GET
    [Fact]
    public async Task GetStelexosByIdInService_ShouldReturnStelexos()
    {
        // Arrange
        var id = 1;
        var thesi = Thesi.Omadarxis;

        var stelexos = new Omadarxis
        {
            Id = id,
            Thesi = thesi,
            LastName = "Μηρτου",
            FirstName = "Δήμητρα",
            Tel = "1234567890",
            Age = 30,
            Sex = Sex.Male,
        };

        _mockStelexiRepository.Setup(r => r.GetStelexosByIdInDb(id)).ReturnsAsync(stelexos);
        _mockMapper.Setup(m => m.Map<StelexosResponse>(stelexos)).Returns(new StelexosResponse()
        {
            Id = id,
            Thesi = thesi,
            Age = 30,
            Tel = "1234567890",
            Sex = Sex.Male,
        });

        // Act
        var result = await _stelexiService.GetStelexosById(id, new());

        // Assert
        Assert.NotNull(result);
        _mockStelexiRepository.Verify(r => r.GetStelexosByIdInDb(id), Times.Once);
        _mockMapper.Verify(m => m.Map<StelexosResponse>(stelexos), Times.Once);
    }

    [Theory]
    [InlineData("Test Name", Thesi.Omadarxis)]
    [InlineData("Test Name", Thesi.Koinotarxis)]
    [InlineData("Test Name", Thesi.Tomearxis)]
    public async Task GetStelexosByNameInService(string name, Thesi thesi)
    {
        // Arrange
        var stelexosDto = new StelexosResponse
        {
            LastName = name.Split(' ')[1],
            FirstName = name.Split(' ')[0],
            Age = 30,
            Tel = "1234567890",
            Sex = Sex.Male,
            XwrosName = "TestXwros",
            Thesi = thesi
        };

        IStelexos stelexos = null!;
        switch (thesi)
        {
            case Thesi.Omadarxis:
                stelexos = new Omadarxis
                {
                    LastName = name.Split(' ')[1],
                    FirstName = name.Split(' ')[0],
                    Age = 30,
                    Sex = Sex.Male,
                    Thesi = thesi,
                    Tel = "1234567890"
                };
                break;
            case Thesi.Koinotarxis:
                stelexos = new Koinotarxis
                {
                    LastName = name.Split(' ')[1],
                    FirstName = name.Split(' ')[0],
                    Age = 30,
                    Sex = Sex.Male,
                    Thesi = thesi,
                    Tel = "1234567890"
                };
                break;
            case Thesi.Tomearxis:
                stelexos = new Tomearxis
                {
                    LastName = name.Split(' ')[1],
                    FirstName = name.Split(' ')[0],
                    Age = 30,
                    Sex = Sex.Male,
                    Thesi = thesi,
                    Tel = "1234567890",
                    Tomeas = new Tomeas { Name = "TestTomea" }
                };
                break;
        }

        _mockStelexiRepository.Setup(r => r.GetStelexosByNameInDb(name, It.IsAny<StelexosQueryParameters>())).ReturnsAsync(stelexos);
        _mockMapper.Setup(m => m.Map<StelexosResponse>(stelexos)).Returns(new StelexosResponse()
        {
            LastName = name.Split(' ')[1],
            FirstName = name.Split(' ')[0],
            Age = 30,
            Sex = Sex.Male,
            Thesi = thesi,
            Tel = "1234567890"
        });

        // Act
        var result = await _stelexiService.GetStelexosByName(name, new());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(name.Split(' ')[1], result.Value.LastName);
        Assert.Equal(name.Split(' ')[0], result.Value.FirstName);

        _mockStelexiRepository.Verify(r => r.GetStelexosByNameInDb(name, It.IsAny<StelexosQueryParameters>()), Times.Once);
        _mockMapper.Verify(m => m.Map<StelexosResponse>(stelexos), Times.Once);
    }

    [Fact]
    public async Task GetAllOmadarxesInService_ShouldReturnOmadarxes()
    {
        // Arrange
        var omadarxisList = new List<Omadarxis>
        {
            new() { LastName = "Test Omadarxis", FirstName = "Test", Sex = Sex.Male, Id = 1, Tel = "12312312" }
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(string.Empty, It.IsAny<StelexosQueryParameters>())).ReturnsAsync(omadarxisList);
        _mockMapper.Setup(m => m.Map<IEnumerable<StelexosResponse>>(omadarxisList)).Returns(
        [
            new() { LastName = "Test Omadarxis", FirstName = "Test", Sex =Sex.Male, Tel = "12312312" }
        ]);

        // Act
        var result = await _stelexiService.GetStelexoiAnaXwro(string.Empty, new());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Test Omadarxis", result.First().LastName);
        Assert.Equal("Test", result.First().FirstName);
        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(string.Empty, It.IsAny<StelexosQueryParameters>()), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<StelexosResponse>>(omadarxisList), Times.Once);
    }

    [Fact]
    public async Task GetAllTomearxesInService_ShouldWork()
    {
        // Arrange
        var tomearxes = new List<Tomearxis>
        {
            new() { Id = 1, LastName = "Doe", FirstName = "John", Sex = Sex.Male, Age = 30, Thesi = Thesi.Tomearxis, Tel = "1234567890", Tomeas = new Tomeas { Name = "TestTomea" } }
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(string.Empty, It.IsAny<StelexosQueryParameters>())).ReturnsAsync(tomearxes);
        _mockMapper.Setup(m => m.Map<IEnumerable<StelexosResponse>>(It.IsAny<IEnumerable<Tomearxis>>())).Returns(new List<StelexosResponse>()
        {
            new()
            {
                Id = 1, LastName = "Doe",FirstName = "John", Sex = Sex.Male, Age = 30, Thesi = Thesi.Omadarxis, Tel = "1234567890"
            }
        });

        // Act
        var result = await _stelexiService.GetStelexoiAnaXwro(string.Empty, new());

        // Assert
        Assert.Single(result);
        Assert.Equal("Doe", result.First().LastName);
        Assert.Equal("John", result.First().FirstName);
        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(string.Empty, It.IsAny<StelexosQueryParameters>()), Times.Once);
    }

    [Fact]
    public async Task GetOmadarxesSeKoinotitaInService_ShouldWork()
    {
        // Arrange
        var koinotitaName = "TestKoinotita";
        var koinotita = new KoinotitaDtoBase
        {
            Name = koinotitaName,
            TomeasName = "Tomeas1"
        };
        var stelexoi = new List<Omadarxis>
        {
            new()
            {
                Id = 1, LastName = "Doe", Age = 30, Thesi = Thesi.Omadarxis, Tel = "1234567890" , FirstName ="John"
            }
        };

        var stelexoiDtos = new List<StelexosDtoBase>
        {
            new() { LastName = "Doe", Age = 30,Tel  = "1234567890", FirstName ="John" }
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(koinotitaName, It.IsAny<StelexosQueryParameters>())).ReturnsAsync(stelexoi);
        _mockMapper.Setup(m => m.Map<IEnumerable<StelexosDtoBase>>(It.IsAny<IEnumerable<Omadarxis>>())).Returns(new List<StelexosResponse>()
        {
            new()
            {
                Id = 1, LastName = "Doe", Age = 30, Thesi = Thesi.Omadarxis, Tel = "1234567890" , FirstName ="John"
            }
        });

        // Act
        var result = await _stelexiService.GetStelexoiAnaXwro(koinotita.Name, new());

        // Assert
        Assert.Single(result);
        Assert.Equal("Doe", result.First().LastName);
        Assert.Equal("John", result.First().FirstName);

        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(koinotitaName, It.IsAny<StelexosQueryParameters>()), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<StelexosResponse>>(stelexoi), Times.Once);
    }

    [Fact]
    public async Task GetAllKoinotarxesInService_ShouldReturnKoinotarxes()
    {
        // Arrange
        var koinotita = new Koinotita { Name = "TestKoinotita", TomeasId = 1, Id = 123 };
        var koinotarxisList = new List<Koinotarxis>
        {
            new() { XwrosName = "Xwros1" , LastName = "Test Koinotarxis", FirstName = "Test", Id = 1 , Tel = "1231231", Thesi = Thesi.Koinotarxis, Age = 29, Sex = Sex.Female, Koinotita = koinotita }
        };
        var koinotarxisDtoList = new List<StelexosResponse>
        {
            new() { XwrosName = "Xwros1" , LastName = "Test Koinotarxis", FirstName = "Test", Tel = "1231231", Sex = Sex.Female, Age = 29, Thesi = Thesi.Koinotarxis, SeAdeia = false }
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(It.IsAny<string>(), It.IsAny<StelexosQueryParameters>()))
                              .ReturnsAsync(koinotarxisList);
        _mockMapper.Setup(m => m.Map<IEnumerable<StelexosResponse>>(koinotarxisList))
                   .Returns(koinotarxisDtoList);

        // Act
        var result = await _stelexiService.GetStelexoiAnaXwro(koinotita.Name, new());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Test Koinotarxis", result.First().LastName);
        Assert.Equal("Test", result.First().FirstName);
        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(koinotita.Name, It.IsAny<StelexosQueryParameters>()), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<StelexosResponse>>(koinotarxisList), Times.Once);
    }

    [Fact]
    public async Task GetKoinotarxesSeTomeaInService_ShouldReturnKoinotarxes()
    {
        // Arrange
        var tomeaDto = new TomeasDtoBase { Name = "TestTomea" };
        var koinotarxisList = new List<Koinotarxis>
        {
            new() { LastName = "Test KoinotarxisL", FirstName = "Test KoinotarxisF", Id = 1 , Tel = "1231231", Sex = Sex.Female, Age = 19 , Thesi = Thesi.Koinotarxis }
        };
        var koinotarxisDtoList = new List<StelexosDtoBase>
        {
            new() { LastName = "Test KoinotarxisL", FirstName = "Test KoinotarxisF", Tel = "1231231" , Sex = Sex.Female}
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(tomeaDto.Name, It.IsAny<StelexosQueryParameters>())).ReturnsAsync(koinotarxisList);
        _mockMapper.Setup(m => m.Map<IEnumerable<StelexosResponse>>(koinotarxisList)).Returns(new List<StelexosResponse>
        {
            new() { LastName = "Test", FirstName = "KoinotarxisF", Tel = "1231231" }
        });

        // Act
        var result = await _stelexiService.GetStelexoiAnaXwro(tomeaDto.Name, new());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("KoinotarxisF", result.First().FirstName);
        Assert.Equal("Test", result.First().LastName);
        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(tomeaDto.Name, It.IsAny<StelexosQueryParameters>()), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<StelexosResponse>>(koinotarxisList), Times.Once);
    }

    [Fact]
    public async Task GetOmadarxesSeKoinotitaInService_ShouldReturnOmadarxisDtos_WhenKoinotitaDtoIsValid()
    {
        // Arrange
        var koinotita = new KoinotitaDtoBase { Name = "TestKoinotita", TomeasName = "TestTomeasName" };
        var omadarxisList = new List<Omadarxis>
        {
            new() { FirstName = "Test", LastName = "Omadarxis", Sex = Sex.Female, Id = 1, Tel = "12313121" }
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(koinotita.Name, It.IsAny<StelexosQueryParameters>())).ReturnsAsync(omadarxisList);
        _mockMapper.Setup(m => m.Map<IEnumerable<StelexosResponse>>(omadarxisList)).Returns(new List<StelexosResponse>()
        {
            new()
            {
                FirstName = "Test", LastName = "Omadarxis", Sex = Sex.Female, Id = 1, Tel = "12312312"
            }

        });

        // Act
        var result = await _stelexiService.GetStelexoiAnaXwro(koinotita.Name, new());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Test", result.First().FirstName);
        Assert.Equal("Omadarxis", result.First().LastName);
        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(koinotita.Name, It.IsAny<StelexosQueryParameters>()), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<StelexosResponse>>(omadarxisList), Times.Once);
    }

    [Fact]
    public async Task GetOmadarxesSeTomeaInService_ShouldReturnOmadarxisDtos_WhenTomeaDtoIsValid()
    {
        // Arrange
        var tomeaDto = new TomeasDtoBase { Name = "TestTomea" };
        var omadarxisList = new List<Omadarxis>
        {
            new() { LastName = "Omadarxis", FirstName  = "John", Sex = Sex.Female, Id = 1, Tel = "122345678" }
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(tomeaDto.Name, It.IsAny<StelexosQueryParameters>())).ReturnsAsync(omadarxisList);
        _mockMapper.Setup(m => m.Map<IEnumerable<StelexosDtoBase>>(omadarxisList)).Returns(new List<StelexosResponse>()
        {
            new()
            {
                Id = 1, LastName = "Omadarxis", FirstName  = "John", Sex = Sex.Female, Age = 30, Thesi = Thesi.Omadarxis, Tel = "1234567890"
            }
        });

        // Act
        var result = await _stelexiService.GetStelexoiAnaXwro(tomeaDto.Name, new());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Omadarxis", result.First().LastName);
        Assert.Equal("John", result.First().FirstName);
        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(tomeaDto.Name, It.IsAny<StelexosQueryParameters>()), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<StelexosResponse>>(omadarxisList), Times.Once);
    }
    #endregion

    #region CREATE
    [Fact]
    public async Task AddStelexosInService_ShouldThrowArgumentNullExceptionOrFalse_WhenDtoIsNull()
    {
        // Arrange
        CreateStelexosRequest? omadarxisDto = null;

        // Act & Assert
        var rest = await _stelexiService.CreateStelexos(omadarxisDto);
        Assert.True(rest.IsFailed);
    }

    [Fact]
    public async Task AddOmadarxisInService_ShouldReturnExpectedResult()
    {
        // Arrange
        var omadarxisDto = new CreateStelexosRequest
        {
            LastName = "Μηρτου",
            FirstName = "Δήμητρα",
            Age = 30,
            Sex = Sex.Male,
            Tel = "11111111",
            XwrosName = "TestXwros",
            Thesi = Thesi.Omadarxis
        };
        var omadarxis = new Omadarxis
        {
            Id = 67,
            LastName = omadarxisDto.LastName,
            FirstName = omadarxisDto.FirstName,
            Age = omadarxisDto.Age,
            Tel = omadarxisDto.Tel,
            Thesi = Thesi.Omadarxis,
            Skini = new Skini { Id = 1 },
            Sex = omadarxisDto.Sex
        };

        _mockMapper.Setup(m => m.Map<Omadarxis>(It.IsAny<CreateStelexosRequest>())).Returns(omadarxis);
        _mockStelexiRepository.Setup(r => r.AddStelexosInDb(It.IsAny<Omadarxis>())).ReturnsAsync(true);
        _stelexosValidator.Setup(v => v.ValidateAsync(It.IsAny<StelexosDtoBase>(), default))
                          .ReturnsAsync(new ValidationResult());

        // Act
        var result = await _stelexiService.CreateStelexos(omadarxisDto);

        // Assert
        Assert.True(result.IsSuccess);
        _mockMapper.Verify(m => m.Map<Omadarxis>(omadarxisDto), Times.Once);
        _mockStelexiRepository.Verify(r => r.AddStelexosInDb(omadarxis), Times.Once);
    }

    [Fact]
    public async Task CreateStelexos_ShouldFail_WhenValidationFails()
    {

    }

    [Fact]
    public async Task CreateStelexos_ShouldFail_WhenRepositoryReturnsFalse()
    {

    }
    #endregion

    #region DELETE
    [Fact]
    public async Task DeleteStelexosInService_ShouldReturnTrue_WhenRepositoryReturnsTrue()
    {
        // Arrange
        var id = 1;
        var thesi = new Thesi { };
        _mockStelexiRepository.Setup(r => r.DeleteStelexosInDb(id)).ReturnsAsync(true);

        // Act
        var result = await _stelexiService.DeleteStelexos(id);

        // Assert
        Assert.True(result.IsSuccess);
        _mockStelexiRepository.Verify(r => r.DeleteStelexosInDb(id), Times.Once);
    }
    #endregion

    [Fact]
    public async Task MoveOmadarxisToAnotherSkiniInService_ShouldReturnTrue_WhenMoveIsSuccessful()
    {
        // Arrange
        var id = 1 + Random.Shared.Next(1, 100);
        var newSkiniName = "NewTestSkini";
        var existingOmadarxis = new Omadarxis
        {
            Id = id,
            Thesi = Thesi.Omadarxis,
            XwrosName = "OldSkini"
        };
        var omadarxisRequest = new UpdateStelexosRequest
        {
            Id = id,
            Thesi = Thesi.Omadarxis,
            FirstName = "FirstName",
            LastName = "LastName",
            Tel = "1234567890",
            XwrosName = newSkiniName,
            Age = 27,
            Sex = Sex.Female
        };

        _mockStelexiRepository.Setup(r => r.GetStelexosByIdInDb(id)).ReturnsAsync(existingOmadarxis);
        _mockStelexiRepository.Setup(r => r.HasPlaceAnotherStelexosInDb(Thesi.Omadarxis, id, newSkiniName)).ReturnsAsync(false);
        _mockStelexiRepository.Setup(r => r.UpdateStelexosInDb(id, It.IsAny<IStelexos>())).ReturnsAsync(true);

        // Act
        var result = await _stelexiService.UpdateStelexos(id, omadarxisRequest);

        // Assert
        Assert.True(result.IsSuccess);
        _mockStelexiRepository.Verify(r => r.HasPlaceAnotherStelexosInDb(Thesi.Omadarxis, id, newSkiniName), Times.Once);
        _mockStelexiRepository.Verify(r => r.UpdateStelexosInDb(id, It.IsAny<IStelexos>()), Times.Once);
    }

    [Fact]
    public async Task MoveOmadarxisToAnotherSkiniInService_ShouldReturnFalse_WhenSomeoneExists()
    {
        // Arrange
        var omadarxisId = 1 + Random.Shared.Next(1, 100);
        var newSkiniName = "NewTestSkini";
        var existingOmadarxis = new Omadarxis
        {
            Id = omadarxisId,
            Thesi = Thesi.Omadarxis,
            XwrosName = "Skini1"
        };
        var omadarxis = new UpdateStelexosRequest
        {
            Id = omadarxisId,
            Thesi = Thesi.Omadarxis,
            FirstName = "FirstNaame",
            LastName = "LastNaame",
            Tel = "12345678290",
            Age = 27,
            Sex = Sex.Male,
            XwrosName = newSkiniName
        };

        _mockStelexiRepository.Setup(r => r.GetStelexosByIdInDb(omadarxisId)).ReturnsAsync(existingOmadarxis);
        _mockStelexiRepository.Setup(r => r.HasPlaceAnotherStelexosInDb(Thesi.Omadarxis, omadarxisId, newSkiniName)).ReturnsAsync(true);
        _mockStelexiRepository.Setup(r => r.UpdateStelexosInDb(omadarxisId, It.IsAny<IStelexos>())).ReturnsAsync(true);

        // Act
        var result = await _stelexiService.UpdateStelexos(omadarxisId, omadarxis);

        // Assert
        Assert.True(result.IsFailed);
        _mockStelexiRepository.Verify(r => r.HasPlaceAnotherStelexosInDb(Thesi.Omadarxis, omadarxisId, newSkiniName), Times.Once);
        _mockStelexiRepository.Verify(r => r.UpdateStelexosInDb(It.IsAny<int>(), It.IsAny<IStelexos>()), Times.Never);
    }

    #region UPDATE
    [Theory]
    [InlineData(Thesi.Omadarxis)]
    [InlineData(Thesi.Koinotarxis)]
    [InlineData(Thesi.Tomearxis)]
    [InlineData(Thesi.Ekpaideutis)]
    public async Task UpdateStelexosInService_ShouldWork(Thesi thesi)
    {
        // Arrange
        var id = 1;

        var createStelexosRequest = new CreateStelexosRequest
        {
            FirstName = "FirstName",
            LastName = "LastName",
            Age = 30,
            XwrosName = "TestXwros",
            Tel = "1234567890",
            Thesi = thesi
        };

        var updateStelexosRequest = new UpdateStelexosRequest
        {
            FirstName = createStelexosRequest.FirstName,
            LastName = createStelexosRequest.LastName,
            Age = 30,
            Tel = "1234567890",
            XwrosName = "TestXwros",
            Thesi = thesi,
            Sex = Sex.Male
        };

        IStelexos stelexos = null!;
        switch (thesi)
        {
            case Thesi.Omadarxis:
                stelexos = new Omadarxis
                {
                    FirstName = createStelexosRequest.FirstName,
                    LastName = createStelexosRequest.LastName,
                    Age = createStelexosRequest.Age,
                    Tel = createStelexosRequest.Tel,
                    Thesi = Thesi.Omadarxis,
                    Id = id,
                    Sex = Sex.Female,
                    Skini = new Skini { Id = 1, Name = "TestSkini" },
                    XwrosName = createStelexosRequest.XwrosName
                };
                _mockMapper.Setup(m => m.Map<Omadarxis>(createStelexosRequest))
                           .Returns((Omadarxis)stelexos);
                break;
            case Thesi.Koinotarxis:
                stelexos = new Koinotarxis
                {
                    FirstName = createStelexosRequest.FirstName,
                    LastName = createStelexosRequest.LastName,
                    Age = createStelexosRequest.Age,
                    Id = id,
                    Tel = createStelexosRequest.Tel,
                    Thesi = Thesi.Koinotarxis,
                    XwrosName = createStelexosRequest.XwrosName,
                    Sex = Sex.Female,
                    Koinotita = new Koinotita { Name = "TestKoinotita" },
                    Omadarxes = []
                };
                _mockMapper.Setup(m => m.Map<Koinotarxis>(createStelexosRequest))
                           .Returns((Koinotarxis)stelexos);
                break;
            case Thesi.Tomearxis:
                stelexos = new Tomearxis
                {
                    FirstName = createStelexosRequest.FirstName,
                    LastName = createStelexosRequest.LastName,
                    Id = id,
                    Age = createStelexosRequest.Age,
                    Sex = Sex.Female,
                    Tel = createStelexosRequest.Tel,
                    Thesi = Thesi.Tomearxis,
                    XwrosName = createStelexosRequest.XwrosName,
                    Koinotarxes = [],
                    Tomeas = new Tomeas { Name = "TestTomea" }
                };
                _mockMapper.Setup(m => m.Map<Tomearxis>(createStelexosRequest))
                           .Returns((Tomearxis)stelexos);
                break;
            case Thesi.Ekpaideutis:
                stelexos = new Ekpaideutis
                {
                    Id = id,
                    FirstName = createStelexosRequest.FirstName,
                    LastName = createStelexosRequest.LastName,
                    Age = createStelexosRequest.Age,
                    Tel = createStelexosRequest.Tel,
                    Thesi = Thesi.Ekpaideutis,
                    Sex = Sex.Female,
                    XwrosName = createStelexosRequest.XwrosName
                };
                _mockMapper.Setup(m => m.Map<Ekpaideutis>(createStelexosRequest))
                           .Returns((Ekpaideutis)stelexos);
                break;
            default:
                throw new ArgumentException("Invalid Thesi", nameof(thesi));
        }
        _mockStelexiRepository.Setup(r => r.AddStelexosInDb(It.Is<IStelexos>(s =>
                                                                             s.LastName == createStelexosRequest.LastName &&
                                                                             s.FirstName == createStelexosRequest.FirstName &&
                                                                             s.Age == createStelexosRequest.Age &&
                                                                             s.Tel == createStelexosRequest.Tel &&
                                                                             s.XwrosName == createStelexosRequest.XwrosName &&
                                                                             s.Thesi == createStelexosRequest.Thesi)))
                              .ReturnsAsync(true)
                              .Verifiable();
        _mockStelexiRepository.Setup(r => r.GetStelexosByIdInDb(id))
                              .ReturnsAsync(stelexos);
        _mockStelexiRepository.Setup(r => r.UpdateStelexosInDb(id, stelexos))
                              .ReturnsAsync(true);

        _mockMapper.Setup(m => m.Map<IStelexos>(createStelexosRequest))
                   .Returns(stelexos);
        _mockMapper.Setup(m => m.Map<IStelexos>(updateStelexosRequest))
                   .Returns(stelexos);
        _mockMapper.Setup(m => m.Map(updateStelexosRequest, stelexos))
                   .Returns(stelexos);

        _stelexosValidator.Setup(v => v.ValidateAsync(It.IsAny<StelexosDtoBase>(), default))
                          .ReturnsAsync(new ValidationResult());

        // Act
        var additionResult = await _stelexiService.CreateStelexos(createStelexosRequest);

        Assert.True(additionResult.IsSuccess);

        var updateResult = await _stelexiService.UpdateStelexos(id, updateStelexosRequest);

        // Assert
        Assert.True(updateResult.IsSuccess);
        _mockStelexiRepository.Verify(r => r.UpdateStelexosInDb(id, stelexos), Times.Once);

        _mockMapper.Verify(m => m.Map<IStelexos>(createStelexosRequest), Times.Once);
        _mockMapper.Verify(m => m.Map(updateStelexosRequest, stelexos), Times.Once);
    }

    [Fact]
    public async Task UpdateStelexos_ShouldFail_WhenValidationFails()
    {

    }

    public async Task UpdateStelexos_ShouldFail_WhenRepositoryReturnsFalse()
    {

    }
    #endregion
}