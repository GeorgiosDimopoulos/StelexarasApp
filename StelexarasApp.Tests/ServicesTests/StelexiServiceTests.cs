using AutoMapper;
using Moq;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.Services;

namespace StelexarasApp.Tests.ServicesTests
{
    public class StelexiServiceTests
    {
        private readonly Mock<StelexiRepository> _mockStelexiRepository;
        private readonly StelexiService _stelexiService;
        private readonly IMapper mapper;

        public StelexiServiceTests()
        {
            _mockStelexiRepository = new Mock<StelexiRepository>(MockBehavior.Strict);
            _stelexiService = new StelexiService(mapper, _mockStelexiRepository.Object);
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

            var service = new StelexiService(null, mockRepository.Object);

            // Act
            var result = await service.GetStelexosByIdInService(id, thesi);

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

            var service = new StelexiService(null, mockRepository.Object);

            // Act
            var result = await service.GetStelexoiAnaThesiInService(thesi);

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

            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IStelexiRepository>();

            mockMapper.Setup(m => m.Map<Stelexos>(stelexosDto)).Returns(stelexos);
            mockRepository.Setup(r => r.AddStelexosInDb(stelexos)).ReturnsAsync(true);

            var service = new StelexiService(mockMapper.Object, mockRepository.Object);

            // Act
            var result = await service.AddStelexosInService(stelexosDto, stelexosDto.Thesi);

            // Assert
            Assert.True(result);
            mockRepository.Verify(r => r.AddStelexosInDb(stelexos), Times.Once);
        }

        [Fact]
        public async Task DeleteStelexosInService_ShouldReturnTrue_WhenRepositoryReturnsTrue()
        {
            // Arrange
            var id = 1;
            var thesi = new Thesi { /* Initialize properties */ };

            var mockRepository = new Mock<IStelexiRepository>();
            mockRepository.Setup(r => r.DeleteStelexosInDb(id, thesi)).ReturnsAsync(true);

            var service = new StelexiService(null, mockRepository.Object);

            // Act
            var result = await service.DeleteStelexosInService(id, thesi);

            // Assert
            Assert.True(result);
            mockRepository.Verify(r => r.DeleteStelexosInDb(id, thesi), Times.Once);
        }


    }
}
