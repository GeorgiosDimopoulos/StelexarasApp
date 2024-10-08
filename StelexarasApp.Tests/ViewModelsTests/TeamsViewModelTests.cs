﻿using Moq;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.ViewModels.TeamsViewModels;
using StelexarasApp.Services.DtosModels.Atoma;

namespace StelexarasApp.Tests.ViewModelsTests;

public class TeamsViewModelTests
{
    private readonly KoinotitaViewModel peopleViewModel;
    private readonly Mock<IPaidiaService> paidiaServiceMock;
    private readonly Mock<ITeamsService> teamsServiceMock;

    public TeamsViewModelTests()
    {
        paidiaServiceMock = new Mock<IPaidiaService>();
        teamsServiceMock = new Mock<ITeamsService>();
        peopleViewModel = new KoinotitaViewModel(paidiaServiceMock.Object, teamsServiceMock.Object);
    }

    [Fact]
    public async void AddPaidiAsync_ShouldWork()
    {
        // Arrange
        var fullName = "Βασιλης Λαμπαδιτης";
        var skiniName = "Πίνδος";
        var paidiType = PaidiType.Kataskinotis;

        // Act
        paidiaServiceMock.Setup(service => service.AddPaidiInService(It.IsAny<PaidiDto>())).ReturnsAsync(true);
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
        paidiaServiceMock.Setup(service => service.AddPaidiInService(It.IsAny<PaidiDto>())).ReturnsAsync(false);
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
        paidiaServiceMock.Setup(service => service.AddPaidiInService(It.IsAny<PaidiDto>())).ReturnsAsync(false);
        var result = await peopleViewModel.AddPaidiAsync(fullName, skiniName, paidiType);

        // Assert
        Assert.False(result);
    }
}
