using Moq;
using StelexarasApp.Services.DtosModels.Atoma;
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
    public async Task OnSaveStelexos_WhenFullNameIsInvalid_ShouldShowAlert()
    {
        // Arrange
        dutyViewModel.FullName = string.Empty;
        dutyViewModel.XwrosName = "XwrosName";
        dutyViewModel.PhoneNumber = "PhoneNumber";
        dutyViewModel.Age = 18;
        dutyViewModel.SelectedThesi = "Omadarxis";

        // Act
        var result = await dutyViewModel.TrySaveStelexosAsync();

        // Assert
        Assert.False(result);
        _mockstaffService.Verify(x => x.AddStelexosInService(It.IsAny<IStelexosDto>()), Times.Never);
    }

    [Fact]
    public async Task OnSaveStelexos_WhenXwrosNameIsInvalid_ShouldShowAlert()
    {
        // Arrange
        dutyViewModel.FullName = "Full Name";
        dutyViewModel.XwrosName = string.Empty;
        dutyViewModel.PhoneNumber = "PhoneNumber";
        dutyViewModel.Age = 18;
        dutyViewModel.SelectedThesi = "Omadarxis";

        // Act
        var result = await dutyViewModel.TrySaveStelexosAsync();

        // Assert
        Assert.False(result);
        _mockstaffService.Verify(x => x.AddStelexosInService(It.IsAny<IStelexosDto>()), Times.Never);
    }

    [Fact]
    public async Task OnSaveStelexos_WhenPhoneNumberIsInvalid_ShouldShowAlert()
    {
        // Arrange
        dutyViewModel.FullName = "Full Name";
        dutyViewModel.XwrosName = "XwrosName";
        dutyViewModel.PhoneNumber = string.Empty;
        dutyViewModel.Age = 18;
        dutyViewModel.SelectedThesi = "Omadarxis";

        // Act
        var result = await dutyViewModel.TrySaveStelexosAsync();

        // Assert
        Assert.False(result);
        _mockstaffService.Verify(x => x.AddStelexosInService(It.IsAny<IStelexosDto>()), Times.Never);
    }

    [Fact]
    public async Task OnSaveStelexos_WhenAgeIsInvalid_ShouldShowAlert()
    {
        // Arrange
        dutyViewModel.FullName = "Full Name";
        dutyViewModel.XwrosName = "XwrosName";
        dutyViewModel.PhoneNumber = "PhoneNumber";
        dutyViewModel.Age = 17;
        dutyViewModel.SelectedThesi = "Omadarxis";

        // Act
        var result = await dutyViewModel.TrySaveStelexosAsync();

        // Assert
        Assert.False(result);
        _mockstaffService.Verify(x => x.AddStelexosInService(It.IsAny<IStelexosDto>()), Times.Never);
    }

    [Fact]
    public async Task OnSaveStelexos_WhenAllFieldsAreValid_ShouldWork()
    {
        // Arrange
        dutyViewModel.FullName = "Full Name";
        dutyViewModel.XwrosName = "XwrosName";
        dutyViewModel.PhoneNumber = "PhoneNumber";
        dutyViewModel.Age = 18;
        dutyViewModel.SelectedThesi = "Omadarxis";

        // Act
        _mockstaffService
            .Setup(x => x.AddStelexosInService(It.IsAny<IStelexosDto>()))
            .ReturnsAsync(true);
        _mockTeamsService
           .Setup(x => x.CheckStelexousXwroNameInService(It.IsAny<IStelexosDto>(), It.IsAny<string>()))
           .ReturnsAsync(true);
        var result = await dutyViewModel.TrySaveStelexosAsync();

        // Assert
        Assert.True(result);
        _mockTeamsService.Verify(x => x.CheckStelexousXwroNameInService(It.IsAny<IStelexosDto>(), It.IsAny<string>()), Times.Once);
    }
}