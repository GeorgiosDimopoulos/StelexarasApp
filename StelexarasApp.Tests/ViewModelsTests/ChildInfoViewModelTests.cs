using Moq;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.ViewModels;
using System.Collections.ObjectModel;
using StelexarasApp.Services.IServices;
using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.Tests.ViewModelsTests
{
    public class ChildInfoViewModelTests
    {
        private readonly Mock<ITeamsService> _mockPeopleService;
        private readonly ChildInfoViewModel _childInfoViewModel;
        private readonly Paidi _paidi;
        private readonly Skini _skini;

        public ChildInfoViewModelTests()
        {
            _mockPeopleService = new Mock<ITeamsService>();

            _skini = GetMockUpSkini();
            _paidi = GetMockUpPaidi(_skini);
            
            _childInfoViewModel = new ChildInfoViewModel(_mockPeopleService.Object, _paidi, new ObservableCollection<Skini>() { _skini });
        }

        [Fact]
        public async Task UpdatePaidiAsync_ShouldUpdatePaidiAndReturnTrue_WhenUpdateSucceeds()
        {
            // Arrange
            var skini = _childInfoViewModel.Skines.FirstOrDefault();
            var newName = "New Name";
            var newAge = 12;

            _mockPeopleService.Setup(s => s.UpdatePaidiInDb(It.IsAny<Paidi>())).ReturnsAsync(true);
            
            // Act
            var result = await _childInfoViewModel.UpdatePaidiAsync("1", newName, skini, newAge);
            
            // Assert
            Assert.True(result);
            Assert.Equal("New Name", _childInfoViewModel.Paidi.FullName);
            Assert.Equal(12, _childInfoViewModel.Paidi.Age);
            Assert.Same(skini, _childInfoViewModel.Paidi.Skini);

            _mockPeopleService.Verify(s => s.UpdatePaidiInDb(It.Is<Paidi>(p => p.FullName == "New Name" && p.Age == 12 && p.Skini == skini)), Times.Once);
            _mockPeopleService.Verify(s => s.GetSkines(), Times.Once);
        }

        [Fact]
        public async Task UpdatePaidiAsync_ShouldNotWorkWhenFullNameNull()
        {
            // Arrange
            _mockPeopleService.Setup(service => service.UpdatePaidiInDb(_paidi)).ReturnsAsync(false);

            // Act
            var result = await _childInfoViewModel.UpdatePaidiAsync(_paidi.Id.ToString(), string.Empty, GetMockUpSkini(), _paidi.Age);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdatePaidiAsync_ShouldNotWorkWhenSkiniNull()
        {
            // Arrange
            _mockPeopleService.Setup(service => service.UpdatePaidiInDb(_paidi)).ReturnsAsync(false);

            // Act
            var result = await _childInfoViewModel.UpdatePaidiAsync(_paidi.Id.ToString(), _paidi.FullName, null, _paidi.Age);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeletePaidiAsync_ShouldUpdateSkines_WhenPaidiIsDeleted()
        {
            // Arrange
            _mockPeopleService.Setup(service => service.DeletePaidiInDb(_paidi)).ReturnsAsync(true);

            // Act
            await _childInfoViewModel.DeletePaidiAsync(_paidi);

            // Assert
            _mockPeopleService.Verify(service => service.DeletePaidiInDb(_paidi), Times.Once);
        }

        private Paidi GetMockUpPaidi(Skini skini)
        {
            return new Paidi
            {
                Id = 1,
                FullName = "Βασιλης Λαμπαδιτης",
                PaidiType = PaidiType.Kataskinotis,
                Age = 10,
                Skini = skini
            };
        }

        private Skini GetMockUpSkini()
        {
            return new Skini { Id = 1, Name = "Πίνδος" };
        }
    }
}
