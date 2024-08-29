using Moq;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.ViewModels;
using System.Collections.ObjectModel;
using StelexarasApp.Services.IServices;
using StelexarasApp.DataAccess.DtosModels;

namespace StelexarasApp.Tests.ViewModelsTests
{
    public class ChildInfoViewModelTests
    {
        private readonly Mock<IPaidiaService> _mockPaidiaService;
        private readonly ChildInfoViewModel _childInfoViewModel;
        private readonly PaidiDto _paidi;
        private readonly Skini _skini;

        public ChildInfoViewModelTests()
        {
            _mockPaidiaService = new Mock<IPaidiaService>();

            _skini = GetMockUpSkini();
            _paidi = GetMockUpPaidi(_skini);

            _childInfoViewModel = new ChildInfoViewModel(_mockPaidiaService.Object, _paidi, new ObservableCollection<Skini>() { _skini });
        }

        [Fact]
        public async Task UpdatePaidiAsync_ShouldUpdatePaidiAndReturnTrue_WhenUpdateSucceeds()
        {
            // Arrange
            var skini = _childInfoViewModel.Skines.FirstOrDefault();
            var newName = "New Name";
            var newAge = 12;

            _mockPaidiaService.Setup(s => s.UpdatePaidiInDb(It.IsAny<PaidiDto>())).ReturnsAsync(true);

            // Act
            var result = await _childInfoViewModel.UpdatePaidiAsync("1", newName, skini, newAge);

            // Assert
            Assert.True(result);
            Assert.Equal("New Name", _childInfoViewModel.PaidiDto.FullName);
            Assert.Equal(12, _childInfoViewModel.PaidiDto.Age);
            Assert.Same(skini.Name, _childInfoViewModel.PaidiDto.SkiniName);

            _mockPaidiaService.Verify(s => s.UpdatePaidiInDb(It.Is<PaidiDto>(p => p.FullName == "New Name" && p.Age == 12 && p.SkiniName == skini.Name)), Times.Once);
            _mockPaidiaService.Verify(s => s.GetSkines(), Times.Once);
        }

        [Fact]
        public async Task UpdatePaidiAsync_ShouldNotWorkWhenFullNameNull()
        {
            // Arrange
            _mockPaidiaService.Setup(service => service.UpdatePaidiInDb(_paidi)).ReturnsAsync(false);

            // Act
            var result = await _childInfoViewModel.UpdatePaidiAsync(_paidi.Id.ToString(), string.Empty, GetMockUpSkini(), _paidi.Age);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdatePaidiAsync_ShouldNotWorkWhenSkiniNull()
        {
            // Arrange
            _mockPaidiaService.Setup(service => service.UpdatePaidiInDb(_paidi)).ReturnsAsync(false);

            // Act
            var result = await _childInfoViewModel.UpdatePaidiAsync(_paidi.Id.ToString(), _paidi.FullName, null, _paidi.Age);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeletePaidiAsync_ShouldUpdateSkines_WhenPaidiIsDeleted()
        {
            // Arrange
            _mockPaidiaService.Setup(service => service.DeletePaidiInDb(_paidi.Id)).ReturnsAsync(true);

            // Act
            await _childInfoViewModel.DeletePaidiAsync(_paidi);

            // Assert
            _mockPaidiaService.Verify(service => service.DeletePaidiInDb(_paidi.Id), Times.Once);
        }

        private PaidiDto GetMockUpPaidi(Skini skini)
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

        private Skini GetMockUpSkini()
        {
            return new Skini { Id = 1, Name = "Πίνδος" };
        }
    }
}
