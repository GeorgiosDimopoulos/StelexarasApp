using Moq;
using StelexarasApp.ViewModels;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.DtosModels.Domi;

namespace StelexarasApp.Tests.ViewModelsTests
{
    public class PaidiInfoViewModelTests
    {
        private readonly Mock<IPaidiaService> _mockPaidiaService;
        private readonly PaidiInfoViewModel _paidiInfoViewModel;
        private readonly PaidiDto _paidi;
        private readonly SkiniDto _skini;

        public PaidiInfoViewModelTests()
        {
            _mockPaidiaService = new Mock<IPaidiaService>();

            _skini = GetMockUpSkini();
            _paidi = GetMockUpPaidi(_skini);

            _paidiInfoViewModel = new PaidiInfoViewModel(_paidi,_mockPaidiaService.Object, [_skini]);
        }

        [Theory]
        [InlineData(null, "SomeSkini", false, "Save failed")]
        [InlineData("ValidName", null, true, "Save successful")]

        public async Task OnSavePaidi_ShouldUpdateStatusMessage(string fullName, string skiniName, bool expectedResult, string expectedMessage)
        {
            // Arrange
            var paidi = new PaidiDto { FullName = fullName };
            var skini = skiniName != null ? GetMockUpSkini() : null;

            _mockPaidiaService.Setup(service => service.UpdatePaidiInDb(paidi)).ReturnsAsync(expectedResult);

            // Act
            await _paidiInfoViewModel.OnSavePaidi();

            // Assert
            _mockPaidiaService.Verify(service => service.UpdatePaidiInDb(paidi), Times.Once);
            Assert.Equal(expectedMessage, _paidiInfoViewModel.StatusMessage);
        }


        [Fact]
        public async Task DeletePaidiAsync_ShouldUpdateSkines_WhenPaidiIsDeleted()
        {
            // Arrange
            _mockPaidiaService.Setup(service => service.DeletePaidiInDb(_paidi.Id)).ReturnsAsync(true);

            // Act
            await _paidiInfoViewModel.DeletePaidiAsync(_paidi.Id);

            // Assert
            _mockPaidiaService.Verify(service => service.DeletePaidiInDb(_paidi.Id), Times.Once);
        }

        private PaidiDto GetMockUpPaidi(SkiniDto skini)
        {
            return new PaidiDto
            {
                Id = 1,
                FullName = "Βασιλης Λαμπαδιτης",
                PaidiType = DataAccess.Models.Atoma.PaidiType.Kataskinotis,
                Age = 10,
                SkiniName = skini.Name
            };
        }

        private SkiniDto GetMockUpSkini()
        {
            return new SkiniDto { Id = 1, Name = "Πίνδος" };
        }
    }
}
