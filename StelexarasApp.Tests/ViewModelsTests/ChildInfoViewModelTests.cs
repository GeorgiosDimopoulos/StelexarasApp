using Moq;
using StelexarasApp.ViewModels;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.DtosModels.Domi;

namespace StelexarasApp.Tests.ViewModelsTests
{
    public class ChildInfoViewModelTests
    {
        private readonly Mock<IPaidiaService> _mockPaidiaService;
        private readonly ChildInfoViewModel _childInfoViewModel;
        private readonly PaidiDto _paidi;
        private readonly SkiniDto _skini;

        public ChildInfoViewModelTests()
        {
            _mockPaidiaService = new Mock<IPaidiaService>();

            _skini = GetMockUpSkini();
            _paidi = GetMockUpPaidi(_skini);

            _childInfoViewModel = new ChildInfoViewModel(_mockPaidiaService.Object, [_skini]);
        }

        [Theory]
        [InlineData(null, "SomeSkini", false)]
        [InlineData("ValidName", null, false)]
        public async Task UpdatePaidiAsync_ShouldReturnExpectedResult(string fullName, string skiniName, bool expectedResult)
        {
            // Arrange
            var paidi = new PaidiDto { FullName = fullName };
            var skini = skiniName != null ? GetMockUpSkini() : null;

            _mockPaidiaService.Setup(service => service.UpdatePaidiInDb(paidi)).ReturnsAsync(expectedResult);

            // Act
            var result = await _childInfoViewModel.UpdatePaidiAsync(paidi, skini);

            // Assert
            Assert.Equal(expectedResult, result);
        }


        [Fact]
        public async Task DeletePaidiAsync_ShouldUpdateSkines_WhenPaidiIsDeleted()
        {
            // Arrange
            _mockPaidiaService.Setup(service => service.DeletePaidiInDb(_paidi.Id)).ReturnsAsync(true);

            // Act
            await _childInfoViewModel.DeletePaidiAsync(_paidi.Id);

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
