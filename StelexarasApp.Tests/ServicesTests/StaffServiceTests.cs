﻿using AutoMapper;
using Moq;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Library.Models.Atoma;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Library.Models.Domi;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Library.Dtos.Domi;
using StelexarasApp.Services.Services;
using FluentValidation;
using StelexarasApp.Services.Validators;

namespace StelexarasApp.Tests.ServicesTests;

public class StaffServiceTests
{
    private readonly Mock<IStaffRepository> _mockStelexiRepository;
    private readonly StaffService _stelexiService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly IMapper _mapper;
    private readonly IValidator<IStelexosDto> stelexosValidator;

    public StaffServiceTests()
    {
        // ToDo: do we need it here?
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Omadarxis, IStelexosDto>();
        });
        _mapper = config.CreateMapper();

        _mockStelexiRepository = new Mock<IStaffRepository>();
        _mockMapper = new Mock<IMapper>();
        stelexosValidator = new StelexosValidator();
        _stelexiService = new StaffService(_mockMapper.Object, _mockStelexiRepository.Object, stelexosValidator);
    }

    [Fact]
    public async Task GetStelexosByIdInService_ShouldReturnStelexos()
    {
        // Arrange
        var id = 1;
        var thesi = Thesi.Omadarxis;

        var expectedStelexosDto = new OmadarxisDto
        {
            FullName = "Test Name",
            Age = 30,
            Sex = Sex.Male,
            Thesi = thesi,
            Tel = "1234567890"
        };
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
        _mockMapper.Setup(m => m.Map<IStelexosDto>(stelexos)).Returns(expectedStelexosDto);

        // Act
        var result = await _stelexiService.GetStelexosByIdInService(id);

        // Assert
        Assert.NotNull(result);
        _mockStelexiRepository.Verify(r => r.GetStelexosByIdInDb(id), Times.Once);
        _mockMapper.Verify(m => m.Map<IStelexosDto>(stelexos), Times.Once);
    }

    [Theory]
    [InlineData("Test Name", Thesi.Omadarxis)]
    [InlineData("Test Name", Thesi.Koinotarxis)]
    [InlineData("Test Name", Thesi.Tomearxis)]
    public async Task GetStelexosByNameInService(string name, Thesi thesi)
    {
        // Arrange
        IStelexosDto stelexosDto = null!;
        switch (thesi)
        {
            case Thesi.Omadarxis:
                stelexosDto = new OmadarxisDto
                {
                    FullName = name,
                    Age = 30,
                    Tel = "1234567890",
                    Thesi = thesi,
                    Sex = Sex.Male,
                    XwrosName = "TestXwros",
                };
                break;
            case Thesi.Tomearxis:
                stelexosDto = new TomearxisDto
                {
                    FullName = name,
                    Age = 30,
                    Tel = "1234567890",
                    Thesi = thesi,
                    Sex = Sex.Male,
                    XwrosName = "TestXwros",
                    KoinotarxesIds = new List<int> { 1, 2 },
                };
                break;
            case Thesi.Koinotarxis:
                stelexosDto = new KoinotarxisDto
                {
                    FullName = name,
                    Age = 30,
                    Tel = "1234567890",
                    Thesi = thesi,
                    Sex = Sex.Male,
                    XwrosName = "TestXwros",
                };
                break;
            default:
                break;
        }

        IStelexos stelexos = null;
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

        _mockStelexiRepository.Setup(r => r.GetStelexosByNameInDb(name, thesi)).ReturnsAsync(stelexos);
        _mockMapper.Setup(m => m.Map<IStelexosDto>(stelexos)).Returns(stelexosDto);

        // Act
        var result = await _stelexiService.GetStelexosByNameInService(name, thesi);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(name, result.FullName);
        _mockStelexiRepository.Verify(r => r.GetStelexosByNameInDb(name, thesi), Times.Once);
        _mockMapper.Verify(m => m.Map<IStelexosDto>(stelexos), Times.Once);
    }

    [Fact]
    public async Task GetAllOmadarxesInService_ShouldReturnOmadarxes()
    {
        // Arrange
        var omadarxisList = new List<Omadarxis>
        {
            new() { FullName = "Test Omadarxis", Id = 1, Tel = "12312312" }
        };
        var omadarxisDtoList = new List<OmadarxisDto>
        {
            new() { FullName = "Test Omadarxis", Tel = "12312312" }
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, string.Empty, new())).ReturnsAsync(omadarxisList);
        _mockMapper.Setup(m => m.Map<IEnumerable<OmadarxisDto>>(omadarxisList)).Returns(omadarxisDtoList);

        // Act
        var result = await _stelexiService.GetAllOmadarxesInService(new());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Test Omadarxis", result.First().FullName);
        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, string.Empty, new()), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<OmadarxisDto>>(omadarxisList), Times.Once);
    }

    [Fact]
    public async Task GetAllKoinotarxesInService_ShouldReturnKoinotarxes()
    {
        // Arrange
        var koinotarxisList = new List<Koinotarxis>
        {
            new() { FullName = "Test Koinotarxis", Id = 1, Tel = "1231231", Thesi = Thesi.Koinotarxis, Age = 29, Sex = Sex.Female}
        };
        var koinotarxisDtoList = new List<KoinotarxisDto>
        {
            new() { FullName = "Test Koinotarxis", Tel = "1231231" }
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(Thesi.Koinotarxis, string.Empty, new())).ReturnsAsync(koinotarxisList);
        _mockMapper.Setup(m => m.Map<IEnumerable<KoinotarxisDto>>(koinotarxisList)).Returns(koinotarxisDtoList);

        // Act
        var result = await _stelexiService.GetAllKoinotarxesInService(new());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Test Koinotarxis", result.First().FullName);
        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(Thesi.Koinotarxis, string.Empty, new()), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<KoinotarxisDto>>(koinotarxisList), Times.Once);
    }

    [Fact]
    public async Task GetKoinotarxesSeTomeaInService_ShouldReturnKoinotarxes()
    {
        // Arrange
        var tomeaDto = new TomeasDto { Name = "TestTomea" };
        var koinotarxisList = new List<Koinotarxis>
        {
            new() { FullName = "Test Koinotarxis", Id = 1 , Tel = "1231231", Sex = Sex.Female, Age = 19 , Thesi = Thesi.Koinotarxis }
        };
        var koinotarxisDtoList = new List<KoinotarxisDto>
        {
            new() { FullName = "Test Koinotarxis", Tel = "1231231" , Sex = Sex.Female}
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(Thesi.Koinotarxis, tomeaDto.Name, new())).ReturnsAsync(koinotarxisList);
        _mockMapper.Setup(m => m.Map<IEnumerable<KoinotarxisDto>>(koinotarxisList)).Returns(koinotarxisDtoList);

        // Act
        var result = await _stelexiService.GetKoinotarxesSeTomeaInService(tomeaDto, new());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Test Koinotarxis", result.First().FullName);
        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(Thesi.Koinotarxis, tomeaDto.Name, new()), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<KoinotarxisDto>>(koinotarxisList), Times.Once);
    }

    [Fact]
    public async Task GetOmadarxesSeKoinotitaInService_ShouldReturnOmadarxisDtos_WhenKoinotitaDtoIsValid()
    {
        // Arrange
        var koinotita = new KoinotitaDto { Name = "TestKoinotita", TomeasName = "TestTomeasName" };
        var omadarxisList = new List<Omadarxis>
        {
            new Omadarxis { FullName = "Test Omadarxis", Id = 1, Tel = "12313121" }
        };
        var omadarxisDtoList = new List<OmadarxisDto>
        {
            new OmadarxisDto { FullName = "Test Omadarxis", Tel = "1235452" }
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, koinotita.Name, new())).ReturnsAsync(omadarxisList);
        _mockMapper.Setup(m => m.Map<IEnumerable<OmadarxisDto>>(omadarxisList)).Returns(omadarxisDtoList);

        // Act
        var result = await _stelexiService.GetOmadarxesSeKoinotitaInService(koinotita, new());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Test Omadarxis", result.First().FullName);
        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, koinotita.Name, new()), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<OmadarxisDto>>(omadarxisList), Times.Once);
    }

    [Fact]
    public async Task GetOmadarxesSeTomeaInService_ShouldReturnOmadarxisDtos_WhenTomeaDtoIsValid()
    {
        // Arrange
        var tomeaDto = new TomeasDto { Name = "TestTomea" };
        var omadarxisList = new List<Omadarxis>
        {
            new Omadarxis { FullName = "Test Omadarxis", Id = 1, Tel = "122345678" }
        };
        var omadarxisDtoList = new List<OmadarxisDto>
        {
            new OmadarxisDto { FullName = "Test Omadarxis"}
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, tomeaDto.Name, new())).ReturnsAsync(omadarxisList);
        _mockMapper.Setup(m => m.Map<IEnumerable<OmadarxisDto>>(omadarxisList)).Returns(omadarxisDtoList);

        // Act
        var result = await _stelexiService.GetOmadarxesSeTomeaInService(tomeaDto, new());

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Test Omadarxis", result.First().FullName);
        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, tomeaDto.Name, new()), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<OmadarxisDto>>(omadarxisList), Times.Once);
    }

    [Fact]
    public async Task AddStelexosInService_ShouldThrowArgumentNullExceptionOrFalse_WhenDtoIsNull()
    {
        // Arrange
        OmadarxisDto? omadarxisDto = null;

        // Act & Assert
        var rest = await _stelexiService.AddStelexosInService(omadarxisDto);
        Assert.False(rest);
    }

    [Fact]
    public async Task AddOmadarxisInService_ShouldReturnExpectedResult()
    {
        // Arrange
        var omadarxisDto = new OmadarxisDto
        {
            FullName = "Ιωαννα Μηρτου",
            Age = 30,
            Sex = Sex.Male,
            Tel = "11111111",
            Thesi = Thesi.Omadarxis,
            XwrosName = "TestXwros",
            Id = 67,
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

        _mockMapper.Setup(m => m.Map<Omadarxis>(It.IsAny<OmadarxisDto>())).Returns(omadarxis);
        _mockStelexiRepository.Setup(r => r.AddStelexosInDb(It.IsAny<Omadarxis>())).ReturnsAsync(true);

        // Act
        var result = await _stelexiService.AddStelexosInService(omadarxisDto);

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
        var result = await _stelexiService.DeleteStelexosByIdInService(id);

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
        var tomearxesDtos = new List<TomearxisDto>
        {
            new() { FullName = "John Doe", Age = 30, Thesi = Thesi.Tomearxis, Tel = "1234567890"}
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(Thesi.Tomearxis, string.Empty, new())).ReturnsAsync(tomearxes);
        _mockMapper.Setup(m => m.Map<IEnumerable<TomearxisDto>>(It.IsAny<IEnumerable<Tomearxis>>())).Returns(tomearxesDtos);

        // Act
        var result = await _stelexiService.GetAllTomearxesInService(new());

        // Assert
        Assert.Single(result);
        Assert.Equal("John Doe", result.First().FullName);
        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(Thesi.Tomearxis, string.Empty, new()), Times.Once);
    }

    [Fact]
    public async Task GetOmadarxesSeKoinotitaInService_ShouldWork()
    {
        // Arrange
        var koinotitaName = "TestKoinotita";
        var koinotita = new KoinotitaDto
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

        var stelexoiDtos = new List<OmadarxisDto>
        {
            new() { FullName = "John Doe", Age = 30, Thesi = Thesi.Omadarxis, Tel = "1234567890" }
        };

        _mockStelexiRepository.Setup(r => r.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, koinotitaName, new())).ReturnsAsync(stelexoi);
        _mockMapper.Setup(m => m.Map<IEnumerable<OmadarxisDto>>(It.IsAny<IEnumerable<Omadarxis>>())).Returns(stelexoiDtos);

        // Act
        var result = await _stelexiService.GetOmadarxesSeKoinotitaInService(koinotita, new());

        // Assert
        Assert.Single(result);
        Assert.Equal("John Doe", result.First().FullName);

        _mockStelexiRepository.Verify(r => r.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, koinotitaName, new()), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<IStelexosDto>>(stelexoi), Times.Once);
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

        _mockStelexiRepository.Setup(r => r.GetStelexosByIdInDb(id)).ReturnsAsync(omadarxis);
        _mockStelexiRepository.Setup(r => r.MoveOmadarxisToAnotherSkiniInDb(omadarxis.Id, newSkini.Name)).ReturnsAsync(true);

        // Act
        var result = await _stelexiService.MoveOmadarxisToAnotherSkiniInService(id, newSkini.Name);

        // Assert
        Assert.True(result);
        _mockStelexiRepository.Verify(r => r.GetStelexosByIdInDb(id), Times.Once);
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
            Skini = new Skini()
        };

        _mockStelexiRepository.Setup(r => r.GetStelexosByIdInDb(omadarxisId)).ReturnsAsync(omadarxis);
        _mockStelexiRepository.Setup(r => r.MoveOmadarxisToAnotherSkiniInDb(omadarxisId, newSkiniName)).ReturnsAsync(false);

        // Act
        var result = await _stelexiService.MoveOmadarxisToAnotherSkiniInService(omadarxisId, newSkiniName);

        // Assert
        Assert.False(result);
        _mockStelexiRepository.Verify(r => r.GetStelexosByIdInDb(omadarxisId), Times.Once);
        _mockStelexiRepository.Verify(r => r.MoveOmadarxisToAnotherSkiniInDb(omadarxisId, newSkiniName), Times.Once);
    }

    [Fact]
    public async Task UpdateStelexosInService_ShouldWork()
    {
        // Arrange
        var id = 1;
        var stelexosDto = new OmadarxisDto
        {
            FullName = "Test Name",
            Age = 30,
            Tel = "1234567890",
        };
        var stelexos = new Omadarxis
        {
            FullName = "Test Name",
            Id = id,
            Age = 30,
            Tel = "1234567890",
            Thesi = Thesi.Omadarxis
        };

        _mockMapper.Setup(m => m.Map<IStelexos>(stelexosDto)).Returns(stelexos);
        _mockStelexiRepository.Setup(r => r.UpdateStelexosInDb(id, stelexos)).ReturnsAsync(true);

        // Act
        var result = await _stelexiService.UpdateStelexosInService(id, stelexosDto);

        // Assert
        Assert.True(result);
        _mockStelexiRepository.Verify(r => r.UpdateStelexosInDb(id, stelexos), Times.Once);
        _mockMapper.Verify(m => m.Map<IStelexos>(stelexosDto), Times.Once);
    }
}
