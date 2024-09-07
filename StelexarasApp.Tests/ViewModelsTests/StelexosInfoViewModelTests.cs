using Moq;
using StelexarasApp.ViewModels;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.Tests.ViewModelsTests
{
    public class StelexosInfoViewModelTests
    {
        private readonly Mock<IStelexiService> _stelexiService;
        private readonly StelexosInfoViewModel stelexosInfoViewModel;
        private readonly StelexosDto stelexosDto;

        public StelexosInfoViewModelTests()
        {
            stelexosDto = GetMockUpStelexos(Thesi.Omadarxis);
            _stelexiService = new Mock<IStelexiService>();
            stelexosInfoViewModel = new StelexosInfoViewModel(stelexosDto, _stelexiService.Object);
        }

        [Theory]
        [InlineData(null, "SomeSkini", false, "Save failed")]
        [InlineData("ValidName", null, true, "Save successful")]

        public async Task OnSaveStelexos_ShouldUpdateStatusMessage(string fullName, string skiniName, bool expectedResult, string expectedMessage)
        {
            // Arrange
            var stelexos = GetMockUpStelexos(Thesi.Omadarxis);
            _stelexiService.Setup(service => service.UpdateStelexosInService(stelexos)).ReturnsAsync(expectedResult);

            // Act
            await stelexosInfoViewModel.OnSaveStelexos();

            // Assert
            _stelexiService.Verify(service => service.UpdateStelexosInService(stelexos), Times.Once);
            Assert.Equal(expectedMessage, stelexosInfoViewModel.StatusMessage);
        }


        [Fact]
        public async Task DeleteStelexosAsync_ShouldUpdateSkines_WhenPaidiIsDeleted()
        {
            // Arrange
            var stelexos = GetMockUpStelexos(Thesi.Omadarxis);
            _stelexiService.Setup(service => service.DeleteStelexosInService(stelexos.Id ?? 0, stelexos.Thesi)).ReturnsAsync(true);

            // Act
            await stelexosInfoViewModel.DeleteStelexos(stelexos);

            // Assert
            _stelexiService.Verify(service => service.DeleteStelexosInService(stelexos.Id ?? 0, stelexos.Thesi), Times.Once);
        }

        private StelexosDto GetMockUpStelexos(Thesi thesi)
        {
            return new StelexosDto
            {
                Age = 20,
                FullName = "Valid Name",
                Sex = Sex.Female,
                Xwros = new Skini(),
                Thesi = thesi
            };
        }
    }
}