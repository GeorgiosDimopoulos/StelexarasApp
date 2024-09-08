using AutoMapper;
using Moq;
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
            _mockStelexiRepository = new Mock<IStelexiRepository>(); // 
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

            var mockRepository = new Mock<IStelexiRepository>();
            mockRepository.Setup(r => r.FindStelexosByIdInDb(id, thesi)).ReturnsAsync(stelexos);

            // Act
            var result = await _stelexiService.GetStelexosByIdInService(id, thesi);

            // Assert
            Assert.Equal(stelexos, result);
            mockRepository.Verify(r => r.FindStelexosByIdInDb(id, thesi), Times.Once);
        }


        [Fact]
        public async Task GetStelexoiAnaThesiInService_ShouldReturnStelexoi_WhenRepositoryReturnsStelexoi()
        {
            // Arrange
            var thesi = new Thesi { /* Initialize properties */ };
            var stelexoi = new List<Omadarxis> 
            { 
                new Omadarxis {}
            };

            var mockRepository = new Mock<IStelexiRepository>();
            mockRepository.Setup(r => r.GetStelexoiAnaThesiFromDb(thesi)).ReturnsAsync(stelexoi);

            // Act
            var result = await _stelexiService.GetStelexoiAnaThesiInService(thesi);

            // Assert
            Assert.Equal(stelexoi, result);
            mockRepository.Verify(r => r.GetStelexoiAnaThesiFromDb(thesi), Times.Once);
        }


        [Fact]
        public async Task AddStelexosInService_ShouldReturnTrue_WhenRepositoryReturnsTrue()
        {
            // Arrange
            var stelexosDto = new StelexosDto 
            {
                FullName = "Test Name",
                Thesi = Thesi.Omadarxis,
            };
            var stelexos = new Omadarxis {};
            /HERE
            _mockMapper.Setup(m => m.Map<Stelexos>(stelexosDto)).Returns(stelexos);
            _mockStelexiRepository.Setup(r => r.AddStelexosInDb(stelexos)).ReturnsAsync(true);

            // Act
            var result = await _stelexiService.AddStelexosInService(stelexosDto, stelexosDto.Thesi);

            // Assert
            Assert.True(result);
            _mockStelexiRepository.Verify(r => r.AddStelexosInDb(stelexos), Times.Once);
        }

        [Fact]
        public async Task DeleteStelexosInService_ShouldReturnTrue_WhenRepositoryReturnsTrue()
        {
            // Arrange
            var id = 1;
            var thesi = new Thesi { /* Initialize properties */ };
            _mockStelexiRepository.Setup(r => r.DeleteStelexosInDb(id, thesi)).ReturnsAsync(true);

            // Act
            var result = await _stelexiService.DeleteStelexosInService(id, thesi);

            // Assert
            Assert.True(result);
            _mockStelexiRepository.Verify(r => r.DeleteStelexosInDb(id, thesi), Times.Once);
        }


    }
}
