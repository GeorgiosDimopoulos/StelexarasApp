using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using StelexarasApp.Library.QueryParameters.People;

namespace StelexarasApp.Tests.ServicesTests;

public class StaffServiceTests
{
    private readonly Mock<IStaffRepository> _mockStelexiRepository;
    private readonly IStaffService<CreateStelexosRequest, UpdateStelexosRequest, StelexosResponse> _stelexiService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IValidator<StelexosDtoBase>> _stelexosValidator;

    public StaffServiceTests()
    {
        _stelexosValidator = new Mock<IValidator<StelexosDtoBase>>();
        _mockStelexiRepository = new Mock<IStaffRepository>();
        _mockMapper = new Mock<IMapper>();
        _stelexiService = new StaffService(_mockMapper.Object, _mockStelexiRepository.Object, _stelexosValidator.Object);
    }

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
            FullName = "Test Name",
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
            FullName = name,
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
                    FullName = name,
                    Age = 30,
                    Sex = Sex.Male,
                    Thesi = thesi,
                    Tel = "1234567890"
                };
                break;
            case Thesi.Koinotarxis:
                stelexos = new Koinotarxis
                {
                    FullName = name,
                    Age = 30,
                    Sex = Sex.Male,
                    Thesi = thesi,
                    Tel = "1234567890"
                };
                break;
            case Thesi.Tomearxis:
                stelexos = new Tomearxis
                {
                    FullName = name,
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
            FullName = name,
            Age = 30,
            Sex = Sex.Male,
            Thesi = thesi,
            Tel = "1234567890"
        });

        // Act
        var result = await _stelexiService.GetStelexosByName(name, new());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(name, result.FullName);
        _mockStelexiRepository.Verify(r => r.GetStelexosByNameInDb(name, It.IsAny<StelexosQueryParameters>()), Times.Once);
        _mockMapper.Verify(m => m.Map<StelexosResponse>(stelexos), Times.Once);
    }

    [Fact]
    public async Task GetAllOmadarxesInService_ShouldReturnOmadarxes()
    {
        // Arrange
        var omadarxisList = new List<Omadarxis>
        {
            new() { FullName = "Test Omadarxis", Id = 1, Tel = "12312312" }
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(string.Empty, It.IsAny<StelexosQueryParameters>())).ReturnsAsync(omadarxisList);
        _mockMapper.Setup(m => m.Map<IEnumerable<StelexosResponse>>(omadarxisList)).Returns(
        [
            new() { FullName = "Test Omadarxis", Tel = "12312312" }
        ]);

        // Act
        var result = await _stelexiService.GetStelexi(string.Empty, new());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Test Omadarxis", result.First().FullName);
        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(string.Empty, It.IsAny<StelexosQueryParameters>()), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<StelexosResponse>>(omadarxisList), Times.Once);
    }

    [Fact]
    public async Task GetAllKoinotarxesInService_ShouldReturnKoinotarxes()
    {
        // Arrange
        var koinotarxisList = new List<Koinotarxis>
        {
            new() { XwrosName = "Xwros1" , FullName = "Test Koinotarxis", Id = 1, Tel = "1231231", Thesi = Thesi.Koinotarxis, Age = 29, Sex = Sex.Female}
        };
        var koinotarxisDtoList = new List<StelexosResponse>
        {
            new() { XwrosName = "Xwros1" , FullName = "Test Koinotarxis", Tel = "1231231" }
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(string.Empty, It.IsAny<StelexosQueryParameters>()))
                              .ReturnsAsync(koinotarxisList);
        _mockMapper.Setup(m => m.Map<IEnumerable<StelexosResponse>>(koinotarxisList))
                   .Returns(koinotarxisDtoList);

        // Act
        var result = await _stelexiService.GetStelexi(string.Empty, new());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Test Koinotarxis", result.First().FullName);
        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(string.Empty, It.IsAny<StelexosQueryParameters>()), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<StelexosResponse>>(koinotarxisList), Times.Once);
    }

    [Fact]
    public async Task GetKoinotarxesSeTomeaInService_ShouldReturnKoinotarxes()
    {
        // Arrange
        var tomeaDto = new TomeasDtoBase { Name = "TestTomea" };
        var koinotarxisList = new List<Koinotarxis>
        {
            new() { FullName = "Test Koinotarxis", Id = 1 , Tel = "1231231", Sex = Sex.Female, Age = 19 , Thesi = Thesi.Koinotarxis }
        };
        var koinotarxisDtoList = new List<StelexosDtoBase>
        {
            new() { FullName = "Test Koinotarxis", Tel = "1231231" , Sex = Sex.Female}
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(tomeaDto.Name, It.IsAny<StelexosQueryParameters>())).ReturnsAsync(koinotarxisList);
        _mockMapper.Setup(m => m.Map<IEnumerable<StelexosResponse>>(koinotarxisList)).Returns(new List<StelexosResponse>
        {
            new() { FullName = "Test Koinotarxis", Tel = "1231231" }
        });

        // Act
        var result = await _stelexiService.GetStelexi(tomeaDto.Name, new());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Test Koinotarxis", result.First().FullName);
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
            new() { FullName = "Test Omadarxis", Id = 1, Tel = "12313121" }
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(koinotita.Name, It.IsAny<StelexosQueryParameters>())).ReturnsAsync(omadarxisList);
        _mockMapper.Setup(m => m.Map<IEnumerable<StelexosResponse>>(omadarxisList)).Returns(new List<StelexosResponse>()
        {
            new()
            {
                FullName = "Test Omadarxis", Id = 1, Tel = "12312312"
            }

        });

        // Act
        var result = await _stelexiService.GetStelexi(koinotita.Name, new());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Test Omadarxis", result.First().FullName);
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
            new() { FullName = "Test Omadarxis", Id = 1, Tel = "122345678" }
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(tomeaDto.Name, It.IsAny<StelexosQueryParameters>())).ReturnsAsync(omadarxisList);
        _mockMapper.Setup(m => m.Map<IEnumerable<StelexosDtoBase>>(omadarxisList)).Returns(new List<StelexosResponse>()
        {
            new()
            {
                Id = 1, FullName = "Test Omadarxis", Age = 30, Thesi = Thesi.Omadarxis, Tel = "1234567890"
            }
        });

        // Act
        var result = await _stelexiService.GetStelexi(tomeaDto.Name, new());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Test Omadarxis", result.First().FullName);
        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(tomeaDto.Name, It.IsAny<StelexosQueryParameters>()), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<StelexosResponse>>(omadarxisList), Times.Once);
    }

    [Fact]
    public async Task AddStelexosInService_ShouldThrowArgumentNullExceptionOrFalse_WhenDtoIsNull()
    {
        // Arrange
        CreateStelexosRequest? omadarxisDto = null;

        // Act & Assert
        var rest = await _stelexiService.CreateStelexos(omadarxisDto);
        Assert.False(rest);
    }

    [Fact]
    public async Task AddOmadarxisInService_ShouldReturnExpectedResult()
    {
        // Arrange
        var omadarxisDto = new CreateStelexosRequest
        {
            FullName = "Ιωαννα Μηρτου",
            Age = 30,
            Sex = Sex.Male,
            Tel = "11111111",
            XwrosName = "TestXwros",
            Thesi = Thesi.Omadarxis
        };
        var omadarxis = new Omadarxis
        {
            Id = 67,
            FullName = omadarxisDto.FullName,
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
        Assert.True(result);
        _mockMapper.Verify(m => m.Map<Omadarxis>(omadarxisDto), Times.Once);
        _mockStelexiRepository.Verify(r => r.AddStelexosInDb(omadarxis), Times.Once);
    }

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
        Assert.True(result);
        _mockStelexiRepository.Verify(r => r.DeleteStelexosInDb(id), Times.Once);
    }

    [Fact]
    public async Task GetAllTomearxesInService_ShouldWork()
    {
        // Arrange
        var tomearxes = new List<Tomearxis>
        {
            new() { Id = 1, FullName = "John Doe", Age = 30, Thesi = Thesi.Tomearxis, Tel = "1234567890", Tomeas = new Tomeas { Name = "TestTomea" } }
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(string.Empty, It.IsAny<StelexosQueryParameters>())).ReturnsAsync(tomearxes);
        _mockMapper.Setup(m => m.Map<IEnumerable<StelexosResponse>>(It.IsAny<IEnumerable<Tomearxis>>())).Returns(new List<StelexosResponse>()
        {
            new()
            {
                Id = 1, FullName = "John Doe", Age = 30, Thesi = Thesi.Omadarxis, Tel = "1234567890"
            }
        });

        // Act
        var result = await _stelexiService.GetStelexi(string.Empty, new());

        // Assert
        Assert.Single(result);
        Assert.Equal("John Doe", result.First().FullName);
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
                Id = 1, FullName = "John Doe", Age = 30, Thesi = Thesi.Omadarxis, Tel = "1234567890"
            }
        };

        var stelexoiDtos = new List<StelexosDtoBase>
        {
            new() { FullName = "John Doe", Age = 30,Tel  = "1234567890" }
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(koinotitaName, It.IsAny<StelexosQueryParameters>())).ReturnsAsync(stelexoi);
        _mockMapper.Setup(m => m.Map<IEnumerable<StelexosDtoBase>>(It.IsAny<IEnumerable<Omadarxis>>())).Returns(new List<StelexosResponse>()
        {
            new()
            {
                Id = 1, FullName = "John Doe", Age = 30, Thesi = Thesi.Omadarxis, Tel = "1234567890"
            }
        });

        // Act
        var result = await _stelexiService.GetStelexi(koinotita.Name, new());

        // Assert
        Assert.Single(result);
        Assert.Equal("John Doe", result.First().FullName);

        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(koinotitaName, It.IsAny<StelexosQueryParameters>()), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<StelexosResponse>>(stelexoi), Times.Once);
    }

    [Fact]
    public async Task MoveOmadarxisToAnotherSkiniInService_ShouldReturnTrue_WhenMoveIsSuccessful()
    {
        // Arrange
        var id = 1;
        var thesi = Thesi.Omadarxis;
        var skini = new Skini { Id = 2, Name = "TestSkini" };
        var newSkini = new Skini { Id = 1, Name = "NewTestSkini" };
        var omadarxis = new Omadarxis
        {
            Id = id,
            Thesi = thesi,
            FullName = "Test Name",
            Tel = "1234567890",
            Skini = skini
        };

        _mockStelexiRepository.Setup(r => r.MoveOmadarxisToAnotherSkiniInDb(omadarxis.Id, newSkini.Name)).ReturnsAsync(true);

        // Act
        var result = await _stelexiService.MoveOmadarxisToAnotherSkiniInService(id, newSkini.Name);

        // Assert
        Assert.True(result);
        _mockStelexiRepository.Verify(r => r.MoveOmadarxisToAnotherSkiniInDb(omadarxis.Id, newSkini.Name), Times.Once);
    }

    [Fact]
    public async Task MoveOmadarxisToAnotherSkiniInService_ShouldReturnFalse_WhenMoveFails()
    {
        // Arrange
        var omadarxisId = 1;
        var newSkiniName = "Skini2";

        var omadarxis = new Omadarxis
        {
            Id = omadarxisId,
            Thesi = Thesi.Omadarxis,
            FullName = "Test Name",
            Tel = "1234567890",
            Age = 27,
            Sex = Sex.Male,
            XwrosName = "TestXwros",
            Skini = new Skini()
        };

        _mockStelexiRepository.Setup(r => r.MoveOmadarxisToAnotherSkiniInDb(omadarxisId, newSkiniName)).ReturnsAsync(false);

        // Act
        var result = await _stelexiService.MoveOmadarxisToAnotherSkiniInService(omadarxisId, newSkiniName);

        // Assert
        Assert.False(result);
        _mockStelexiRepository.Verify(r => r.MoveOmadarxisToAnotherSkiniInDb(omadarxisId, newSkiniName), Times.Once);
    }

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
            FullName = "Test Name",
            Age = 30,
            XwrosName = "TestXwros",
            Tel = "1234567890",
            Thesi = thesi
        };

        var updateStelexosRequest = new UpdateStelexosRequest
        {
            FullName = "Test Name",
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
                    FullName = createStelexosRequest.FullName,
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
                    FullName = createStelexosRequest.FullName,
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
                    FullName = createStelexosRequest.FullName,
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
                    FullName = createStelexosRequest.FullName,
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
                                                                             s.FullName == createStelexosRequest.FullName &&
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
                          .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        // Act
        var additionResult = await _stelexiService.CreateStelexos(createStelexosRequest);

        Assert.True(additionResult);

        var updateResult = await _stelexiService.UpdateStelexos(id, updateStelexosRequest);

        // Assert
        Assert.True(updateResult);
        _mockStelexiRepository.Verify(r => r.UpdateStelexosInDb(id, stelexos), Times.Once);

        _mockMapper.Verify(m => m.Map<IStelexos>(createStelexosRequest), Times.Once);
        _mockMapper.Verify(m => m.Map(updateStelexosRequest, stelexos), Times.Once);
    }
}
