using AutoMapper;
using Moq;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.Services;

namespace StelexarasApp.Tests.ServicesTests
{
    public class PaidiaServiceTests
    {
        private readonly Mock<PaidiRepository> _mockPaidiRepository;
        private readonly PaidiaService _paidiaService;
        private readonly IMapper mapper;

        public PaidiaServiceTests()
        {
            _mockPaidiRepository = new Mock<PaidiRepository>(MockBehavior.Strict);
            _paidiaService = new PaidiaService(_mockPaidiRepository.Object, mapper);
        }

        [Fact]
        public async Task AddEkpaideuomenos_ShouldReturnTrue_WhenSuccessful()
        {
            // Arrange
            var paidiDto = new PaidiDto
            {
                Id = 1,
                FullName = "Test",
                PaidiType = PaidiType.Ekpaideuomenos
            };

            var paidi = mapper.Map<Paidi>(paidiDto);
            _mockPaidiRepository.Setup(repo => repo.AddPaidiInDb(paidi)).ReturnsAsync(true);

            // Act
            var result = await _paidiaService.AddPaidiInDbAsync(paidiDto);

            // Assert
            Assert.True(result);
            _mockPaidiRepository.Verify(repo => repo.AddPaidiInDb(paidi), Times.Once);
        }

        [Theory]
        [InlineData(PaidiType.Kataskinotis, 2)]
        [InlineData(PaidiType.Ekpaideuomenos, 1)]
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
            var result = await _paidiaService.GetPaidia(paidiType);

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
            var paidiId = 1;
            var paidi = new Paidi
            {
                Id = paidiId,
                FullName = "Test Name",
                PaidiType = PaidiType.Ekpaideuomenos
            };
            var expectedResult = true;
            
            _mockPaidiRepository.Setup(service => service.DeletePaidiInDb(paidi)).ReturnsAsync(expectedResult);
            var result = await _paidiaService.DeletePaidiInDb(paidiId);

            Assert.True(result);
            _mockPaidiRepository.Verify(service => service.DeletePaidiInDb(paidi), Times.Once);
        }

        [Theory]
        [InlineData(1, "John Doe", 30, PaidiType.Kataskinotis, true)]
        [InlineData(2, null, 0, PaidiType.Unknown, false)]
        public async Task GetPaidiById_ShouldReturnExpectedResult(int paidiId, string expectedFullName, int expectedAge, PaidiType expectedType, bool shouldExist)
        {
            // Arrange
            Paidi? expectedPaidi = shouldExist ? new Paidi
            {
                Id = paidiId,
                FullName = expectedFullName,
                Age = expectedAge,
                PaidiType = expectedType
            } : null;

            _mockPaidiRepository.Setup(repo => repo.GetPaidiByIdFromDb(paidiId))
                .ReturnsAsync(expectedPaidi);

            // Act
            var result = await _paidiaService.GetPaidiById(paidiId);

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
}
