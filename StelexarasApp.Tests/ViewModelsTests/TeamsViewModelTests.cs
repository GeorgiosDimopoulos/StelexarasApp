using Moq;
using StelexarasApp.Services.DtosModels;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.Services.IServices;
using StelexarasApp.ViewModels;
using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.Tests.ViewModelsTests
{
    public class TeamsViewModelTests
    {
        private readonly TeamsViewModel peopleViewModel;
        private readonly Mock<IPaidiaService> paidiaServiceMock;

        public TeamsViewModelTests()
        {
            paidiaServiceMock = new Mock<IPaidiaService>();
            peopleViewModel = new TeamsViewModel(paidiaServiceMock.Object, It.IsAny<EidosXwrou>);
        }

        [Fact]
        public async void AddPaidiAsync_ShouldWork()
        {
            // Arrange
            var fullName = "Βασιλης Λαμπαδιτης";
            var skiniName = "Πίνδος";
            var paidiType = PaidiType.Kataskinotis;

            // Act
            paidiaServiceMock.Setup(service => service.AddPaidiInDbAsync(It.IsAny<PaidiDto>())).ReturnsAsync(true);
            var result = await peopleViewModel.AddPaidiAsync(fullName, skiniName, paidiType);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void AddPaidiAsync_ShouldNotWorkWhenFullNameNull()
        {
            // Arrange
            var fullName = string.Empty;
            var skiniName = "Πίνδος";
            var paidiType = PaidiType.Kataskinotis;

            // Act
            paidiaServiceMock.Setup(service => service.AddPaidiInDbAsync(It.IsAny<PaidiDto>())).ReturnsAsync(false);
            var result = await peopleViewModel.AddPaidiAsync(fullName, skiniName, paidiType);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void AddPaidiAsync_ShouldNotWorkWhenSkiniNameNull()
        {
            // Arrange
            string fullName = "Βασιλης Λαμπαδιτης";
            string skiniName = string.Empty;
            PaidiType paidiType = PaidiType.Kataskinotis;

            // Act
            paidiaServiceMock.Setup(service => service.AddPaidiInDbAsync(It.IsAny<PaidiDto>())).ReturnsAsync(false);
            var result = await peopleViewModel.AddPaidiAsync(fullName, skiniName, paidiType);

            // Assert
            Assert.False(result);
        }
    }
}
