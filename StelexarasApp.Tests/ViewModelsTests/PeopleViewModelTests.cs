using StelexarasApp.Services.IServices;
using StelexarasApp.ViewModels;

namespace StelexarasApp.Tests.ViewModelsTests
{
    public class PeopleViewModelTests
    {
        private readonly PeopleViewModel peopleViewModel;

        public PeopleViewModelTests(IPeopleService peopleService)
        {
            peopleViewModel = new PeopleViewModel(peopleService);
        }

        public async void AddPaidiAsync_ShouldWork() 
        {
            // Arrange
            string fullName = "Βασιλης Λαμπαδιτης";
            string skiniName = "Πίνδος";

            // Act
            var result = await peopleViewModel.AddPaidiAsync(fullName, skiniName);

            // Assert
            Assert.True(result);
        }

        public async void AddPaidiAsync_ShouldNotWorkWhenFullNameNull()
        {
            // Arrange
            string fullName = string.Empty;
            string skiniName = "Πίνδος";

            // Act
            var result = await peopleViewModel.AddPaidiAsync(fullName, skiniName);

            // Assert
            Assert.False(result);
        }

        public async void AddPaidiAsync_ShouldNotWorkWhenSkiniNameNull()
        {
            // Arrange
            string fullName = "Βασιλης Λαμπαδιτης";
            string skiniName = string.Empty;

            // Act
            var result = await peopleViewModel.AddPaidiAsync(fullName, skiniName);

            // Assert
            Assert.False(result);
        }
    }
}
