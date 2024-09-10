using Moq;
using StelexarasApp.ViewModels;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.DtosModels.Domi;

namespace StelexarasApp.Tests.ViewModelsTests;

public class PaidiInfoViewModelTests
{
    private readonly Mock<IPaidiaService> _mockPaidiaService;
    private readonly PaidiInfoViewModel _paidiInfoViewModel;
    private readonly PaidiDto paidiDto;

    public PaidiInfoViewModelTests()
    {
        _mockPaidiaService = new Mock<IPaidiaService>();
        paidiDto = GetMockUpPaidi(GetMockUpSkini(), "Paidi Name");
        _paidiInfoViewModel = new PaidiInfoViewModel(paidiDto, _mockPaidiaService.Object, [GetMockUpSkini()]);
    }

    [Theory]
    [InlineData(null, false, "Save failed")]
    //[InlineData("Some Name", true, "Save successful")]

    public async Task OnSavePaidi_ShouldUpdateStatusMessage(string paidiName, bool expectedResult, string expectedMessage)
    {
        // Arrange
        _mockPaidiaService.Setup(service => service.UpdatePaidiInDb(paidiDto)).ReturnsAsync(expectedResult);

        // Act
        _paidiInfoViewModel.PaidiDto.FullName = paidiName;
        var result = await _paidiInfoViewModel.OnSavePaidi();

        // Assert
        _mockPaidiaService.Verify(service => service.UpdatePaidiInDb(paidiDto), Times.Once);
        Assert.Equal(expectedMessage, _paidiInfoViewModel.StatusMessage);
        Assert.Equal(result, expectedResult);
    }

    [Fact]
    public async Task DeletePaidiAsync_ShouldUpdateSkines_WhenPaidiIsDeleted()
    {
        // Arrange
        _mockPaidiaService.Setup(service => service.DeletePaidiInDb(paidiDto.Id)).ReturnsAsync(true);

        // Act
        var result = await _paidiInfoViewModel.DeletePaidiAsync(paidiDto.Id);

        // Assert
        _mockPaidiaService.Verify(service => service.DeletePaidiInDb(paidiDto.Id), Times.Once);
        Assert.Equal("Delete successful", _paidiInfoViewModel.StatusMessage);
        Assert.True(result);
    }

    private static PaidiDto GetMockUpPaidi(SkiniDto skini, string name)
    {
        return new PaidiDto
        {
            Id = 1,
            FullName = name,
            PaidiType = DataAccess.Models.Atoma.PaidiType.Kataskinotis,
            Age = 10,
            SkiniName = skini.Name
        };
    }

    private static SkiniDto GetMockUpSkini()
    {
        return new SkiniDto { Id = 1, Name = "Πίνδος" };
    }
}
