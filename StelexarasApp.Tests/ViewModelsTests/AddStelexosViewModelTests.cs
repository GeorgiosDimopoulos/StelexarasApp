using Moq;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.ViewModels.PeopleViewModels;

namespace StelexarasApp.Tests.ViewModelsTests;

public class AddStelexosViewModelTests
{
    private readonly AddStelexosViewModel dutyViewModel;
    private readonly Mock<IStaffService> _mockstaffService;
    private readonly Mock<ITeamsService> _mockTeamsService;

    public AddStelexosViewModelTests()
    {
        _mockstaffService = new Mock<IStaffService>();
        _mockTeamsService = new Mock<ITeamsService>();
        dutyViewModel = new AddStelexosViewModel(_mockstaffService.Object, _mockTeamsService.Object);
    }

    [Fact]
    public void OnSaveStelexos_WhenFullNameIsInvalid_ShouldShowAlert()
    {
        // Arrange
        dutyViewModel.FullName = string.Empty;
        dutyViewModel.XwrosName = "XwrosName";
        dutyViewModel.PhoneNumber = "PhoneNumber";
        dutyViewModel.Age = 18;
        dutyViewModel.SelectedThesi = "Omadarxis";

        // Act
        dutyViewModel.SaveCommand.Execute(null);

        // Assert
        _mockstaffService.Verify(x => x.AddStelexosInService(It.IsAny<StelexosDto>()), Times.Never);
    }

    [Fact]
    public void OnSaveStelexos_WhenXwrosNameIsInvalid_ShouldShowAlert()
    {
        // Arrange
        dutyViewModel.FullName = "FullName";
        dutyViewModel.XwrosName = string.Empty;
        dutyViewModel.PhoneNumber = "PhoneNumber";
        dutyViewModel.Age = 18;
        dutyViewModel.SelectedThesi = "Omadarxis";

        // Act
        dutyViewModel.SaveCommand.Execute(null);

        // Assert
        _mockstaffService.Verify(x => x.AddStelexosInService(It.IsAny<StelexosDto>()), Times.Never);
    }

    [Fact]
    public void OnSaveStelexos_WhenPhoneNumberIsInvalid_ShouldShowAlert()
    {
        // Arrange
        dutyViewModel.FullName = "Full Name";
        dutyViewModel.XwrosName = "XwrosName";
        dutyViewModel.PhoneNumber = string.Empty;
        dutyViewModel.Age = 18;
        dutyViewModel.SelectedThesi = "Omadarxis";

        // Act
        dutyViewModel.SaveCommand.Execute(null);

        // Assert
        _mockstaffService.Verify(x => x.AddStelexosInService(It.IsAny<StelexosDto>()), Times.Never);
    }

    [Fact]
    public void OnSaveStelexos_WhenAgeIsInvalid_ShouldShowAlert()
    {
        // Arrange
        dutyViewModel.FullName = "Full Name";
        dutyViewModel.XwrosName = "XwrosName";
        dutyViewModel.PhoneNumber = "PhoneNumber";
        dutyViewModel.Age = 17;
        dutyViewModel.SelectedThesi = "Omadarxis";

        // Act
        dutyViewModel.SaveCommand.Execute(null);

        // Assert
        _mockstaffService.Verify(x => x.AddStelexosInService(It.IsAny<StelexosDto>()), Times.Never);
    }

    [Fact]
    public void OnSaveStelexos_WhenAllFieldsAreValid_ShouldAddStelexos()
    {
        // Arrange
        dutyViewModel.FullName = "Full Name";
        dutyViewModel.XwrosName = "XwrosName";
        dutyViewModel.PhoneNumber = "PhoneNumber";
        dutyViewModel.Age = 18;
        dutyViewModel.SelectedThesi = "Omadarxis";

        // Act
        dutyViewModel.SaveCommand.Execute(null);

        // Assert
        _mockstaffService.Verify(x => x.AddStelexosInService(It.IsAny<StelexosDto>()), Times.Once);
    }
}