using AutoMapper;
using Moq;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.Services;

namespace StelexarasApp.Tests.ServicesTests
{
    public class StelexiServiceTests
    {
        private readonly Mock<IStelexiRepository> _mockStelexiRepository;
        private readonly StelexiService _stelexiService;
        private readonly Mock<IMapper> _mockMapper;
        public StelexiServiceTests()
        {
            _mockStelexiRepository = new Mock<IStelexiRepository>();
            _mockMapper = new Mock<IMapper>();
            _stelexiService = new StelexiService(_mockMapper.Object, _mockStelexiRepository.Object);
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
                new Omadarxis { Id = 1, FullName = "John Doe", Age = 30, Thesi = Thesi.Omadarxis},
            };

            _mockStelexiRepository.Setup(r => r.GetStelexoiAnaThesiFromDb(Thesi.Omadarxis)).ReturnsAsync(stelexoi);

            // Act
            var result = await _stelexiService.GetStelexoiAnaThesiInService(Thesi.Omadarxis);

            // Assert
            Assert.Equal(stelexoi, result);
            _mockStelexiRepository.Verify(r => r.GetStelexoiAnaThesiFromDb(Thesi.Omadarxis), Times.Once);
        }

        [Theory]
        [InlineData("Test Name", "TestXwros", Thesi.Omadarxis, 2, true, true)]
        [InlineData("Test Name", "TestXwros", Thesi.Omadarxis, 1, false, false)]
        public async Task AddStelexosInService_ShouldReturnExpectedResult(
            string fullName, string xwrosName, Thesi thesi, int id, bool repositoryResult, bool expectedResult)
        {
            // Arrange
            var stelexosDto = new OmadarxisDto
            {
                FullName = fullName,
                XwrosName = xwrosName,
                Age = 30,
                Sex = Sex.Male,
                Id = id,
                Thesi = thesi
            };
            var stelexos = new Omadarxis
            {
                Id = id,
                FullName = fullName,
                Age = 30,
                Thesi = thesi
            };
            _mockMapper.Setup(m => m.Map<Omadarxis>(stelexosDto)).Returns(stelexos);

            // _mockStelexiRepository.Setup(repo => repo.AddStelexosInDb(stelexos)).ReturnsAsync(repositoryResult);
            _mockStelexiRepository.Setup(repo => repo.AddStelexosInDb(It.IsAny<Omadarxis>())).ReturnsAsync(repositoryResult);

            // Act
            var result = await _stelexiService.AddStelexosInService(stelexosDto, stelexosDto.Thesi);

            // Assert
            Assert.Equal(expectedResult, result);

            // _mockStelexiRepository.Verify(r => r.AddStelexosInDb(stelexos), Times.Once);
            _mockStelexiRepository.Verify(r => r.AddStelexosInDb(It.Is<Omadarxis>(
                s => s.FullName == fullName && s.Id == id && s.Thesi == thesi)), Times.Once);
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
}
