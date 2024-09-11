using Moq;
using StelexarasApp.ViewModels;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.Tests.ViewModelsTests;

public class StelexosInfoViewModelTests
{
    private readonly Mock<IStaffService> _mockstelexiService;
    private readonly StelexosInfoViewModel stelexosInfoViewModel;
    private readonly StelexosDto stelexosDto;

    public StelexosInfoViewModelTests()
    {
        stelexosDto = GetMockUpStelexos(Thesi.Omadarxis);
        _mockstelexiService = new Mock<IStaffService>();
        stelexosInfoViewModel = new StelexosInfoViewModel(stelexosDto, _mockstelexiService.Object);

    }

    [Theory]
    [InlineData(null, "xwros", false, "Save failed")]
    [InlineData("Valid Name", "xwros", true, "Save successful")]
    public async Task OnSaveStelexos_ShouldUpdateStatusMessage(string fullName, string xwrosName, bool expectedResult, string expectedMessage)
    {
        // Arrange
        var stelexosDto = GetMockUpStelexos(Thesi.Omadarxis, fullName, xwrosName);
        _mockstelexiService.Setup(service => service.UpdateStelexosInService(stelexosDto)).ReturnsAsync(expectedResult);
        stelexosInfoViewModel.StelexosDto = stelexosDto;

        // Act
        await stelexosInfoViewModel.OnSaveStelexos();

        // Assert
        _mockstelexiService.Verify(service => service.UpdateStelexosInService(stelexosDto), Times.Once);
        Assert.Equal(expectedMessage, stelexosInfoViewModel.StatusMessage);
    }

    [Fact]
    public async Task DeleteStelexosAsync_ShouldUpdateSkines_WhenPaidiIsDeleted()
    {
        // Arrange
        var stelexos = GetMockUpStelexos(Thesi.Omadarxis, "Test Name", "Test Xwros");
        _mockstelexiService.Setup(service => service.DeleteStelexosInService(stelexos.Id ?? 1, stelexos.Thesi)).ReturnsAsync(true);

        // Act
        await stelexosInfoViewModel.DeleteStelexos(stelexos);

        // Assert
        _mockstelexiService.Verify(service => service.DeleteStelexosInService(stelexos.Id ?? 1, stelexos.Thesi), Times.Once);
    }

    private StelexosDto GetMockUpStelexos(Thesi? thesi = Thesi.Omadarxis, string name = "Some name", string xwrosName = "someXwros")
    {
        return new StelexosDto
        {
            Age = 20,
            FullName = name,
            Sex = Sex.Female,
            XwrosName = xwrosName,
            Thesi = thesi ?? Thesi.None,
        };
    }
}