using Moq;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Paidia;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.IServices;
using StelexarasApp.ViewModels;

namespace StelexarasApp.Tests.ViewModelsTests
{
    public class PeopleViewModelTests
    {
        private readonly PeopleViewModel peopleViewModel;
        private readonly Mock<IPeopleService> mockPeopleService;


        public PeopleViewModelTests(IPeopleService peopleService)
        {
            mockPeopleService = new Mock<IPeopleService>();
            peopleViewModel = new PeopleViewModel(peopleService);
        }

        [Fact]
        public async void AddPaidiAsync_ShouldWork()
        {
            // Arrange
            var fullName = "Βασιλης Λαμπαδιτης";
            var skiniName = "Πίνδος";
            var paidiType = PaidiType.Kataskinotis;

            // Act
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
            var result = await peopleViewModel.AddPaidiAsync(fullName, skiniName, paidiType);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateKataskinotisAsync_ShouldWork()
        {
            // Arrange
            int paidiId = 1;
            string fullName = "Βασιλης Λαμπαδιτης";
            string skiniName = "Πίνδος";
            PaidiType type = PaidiType.Kataskinotis;
            int age = 10;
            Sex sex = Sex.Male;

            var paidi = new Kataskinotis { Id = paidiId, FullName = fullName, Skini = new Skini { Name = skiniName } };
            mockPeopleService.Setup(service => service.GetPaidiByIdAsync(paidiId, type)).ReturnsAsync(paidi);
            mockPeopleService.Setup(service => service.UpdatePaidiInDbAsync(paidi)).ReturnsAsync(true);

            // Act
            var result = await peopleViewModel.UpdatePaidiAsync(paidiId.ToString(), fullName, type, skiniName, age, sex);
            // Assert
            Assert.True(result);
            mockPeopleService.Verify(service => service.GetPaidiByIdAsync(paidiId, type), Times.Once);
            mockPeopleService.Verify(service => service.UpdatePaidiInDbAsync(paidi), Times.Once);
        }

        [Fact]
        public async Task UpdatePaidiAsync_ShouldNotWorkWhenFullNameNull()
        {
            // Arrange
            int paidiId = 1;
            string fullName = string.Empty;
            string skiniName = "Πίνδος";
            PaidiType type = PaidiType.Kataskinotis;
            int age = 10;
            Sex sex = Sex.Male;
            
            // Act
            var result = await peopleViewModel.UpdatePaidiAsync(paidiId.ToString(), fullName, type, skiniName, age, sex);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdatePaidiAsync_ShouldNotWorkWhenSkiniNameNull()
        {
            // Arrange
            int paidiId = 1;
            string fullName = "Βασιλης Λαμπαδιτης";
            string skiniName = string.Empty;
            PaidiType type = PaidiType.Kataskinotis;
            int age = 10;
            Sex sex = Sex.Male;

            // Act
            var result = await peopleViewModel.UpdatePaidiAsync(paidiId.ToString(), fullName, type, skiniName, age, sex);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdatePaidiAsync_ShouldNotWorkWhenPaidiNotFound()
        {
            // Arrange
            int paidiId = 1;
            string fullName = "Βασιλης Λαμπαδιτης";
            string skiniName = "Πίνδος";
            PaidiType type = PaidiType.Kataskinotis;
            int age = 10;
            Sex sex = Sex.Male;

            mockPeopleService.Setup(service => service.GetPaidiByIdAsync(paidiId, type)).ReturnsAsync((Paidi)null);

            // Act
            var result = await peopleViewModel.UpdatePaidiAsync(paidiId.ToString(), fullName, type, skiniName, age, sex);

            // Assert
            Assert.False(result);
            mockPeopleService.Verify(service => service.GetPaidiByIdAsync(paidiId, type), Times.Once);
            mockPeopleService.Verify(service => service.UpdatePaidiInDbAsync(It.IsAny<Paidi>()), Times.Never);
        }
    }
}
