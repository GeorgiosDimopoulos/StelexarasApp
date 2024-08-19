using Moq;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Paidia;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.IServices;
using StelexarasApp.ViewModels;
using System.Collections.ObjectModel;

namespace StelexarasApp.Tests.ViewModelsTests
{
    public class TeamsViewModelTests
    {
        private readonly TeamsViewModel peopleViewModel;
        private readonly Mock<ITeamsService> mockPeopleService;


        public TeamsViewModelTests()
        {
            mockPeopleService = new Mock<ITeamsService>();
            peopleViewModel = new TeamsViewModel(mockPeopleService.Object);
        }

        [Fact]
        public async void AddPaidiAsync_ShouldWork()
        {
            // Arrange
            var fullName = "Βασιλης Λαμπαδιτης";
            var skiniName = "Πίνδος";
            var paidiType = PaidiType.Kataskinotis;

            // Act
            mockPeopleService.Setup(service => service.AddPaidiInDbAsync(It.IsAny<Paidi>())).ReturnsAsync(true);
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
            mockPeopleService.Setup(service => service.AddPaidiInDbAsync(It.IsAny<Paidi>())).ReturnsAsync(false);
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
            mockPeopleService.Setup(service => service.AddPaidiInDbAsync(It.IsAny<Paidi>())).ReturnsAsync(false);
            var result = await peopleViewModel.AddPaidiAsync(fullName, skiniName, paidiType);

            // Assert
            Assert.False(result);
        }
    }
}
