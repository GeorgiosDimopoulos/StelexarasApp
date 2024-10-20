﻿using Moq;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.ViewModels;

namespace StelexarasApp.Tests.ViewModelsTests;

public class DutyViewModelTests
{
    private readonly DutyViewModel dutyViewModel;
    private readonly Mock<IDutyService> _mockDutyService;

    public DutyViewModelTests()
    {
        _mockDutyService = new Mock<IDutyService>();
        dutyViewModel = new DutyViewModel(_mockDutyService.Object);
    }

    [Fact]
    public async Task DeleteDuty_ShouldReturnTrue_WhenDutyIsDeletedSuccessfully()
    {
        // Arrange
        _mockDutyService.Setup(service => service.DeleteDutyInService(1)).ReturnsAsync(true);

        // Act
        var result = await dutyViewModel.DeleteDuty(1);

        // Assert
        Assert.True(result);
        _mockDutyService.Verify(service => service.DeleteDutyInService(1), Times.Once);
    }

    [Fact]
    public async void UpdateDuty_ShouldReturnFalse_WhenDutyDoesNotExistInVM()
    {
        // Arrange
        var oldDutyName = "duty Name";
        var duty = new Duty { Name = oldDutyName }; 
        var newDutyName = "Some New Name";

        _mockDutyService.Setup(s => s.UpdateDutyInService(newDutyName, It.IsAny<Duty>())).ReturnsAsync(false);
        dutyViewModel.Duties = new System.Collections.ObjectModel.ObservableCollection<Duty>();

        // Act
        var result = await dutyViewModel.UpdateDuty(It.IsAny<Duty>(), newDutyName);

        // Assert
        Assert.False(result);
        _mockDutyService.Verify(s => s.UpdateDutyInService("duty Name", It.Is<Duty>(d => d.Name == oldDutyName)), Times.Never);
    }

    [Fact]
    public async void AddDuty_ShouldReturnTrue_WhenDutyIsAddedSuccessfully()
    {
        // Arrange
        var dutyName = "New Duty";

        _mockDutyService.Setup(s => s.AddDutyInService(It.IsAny<Duty>())).ReturnsAsync(true);

        // Act
        var result = await dutyViewModel.AddDuty(dutyName);

        // Assert
        Assert.True(result);
        _mockDutyService.Verify(s => s.AddDutyInService(It.Is<Duty>(d => d.Name == dutyName)), Times.Once);
        Assert.Contains(dutyViewModel.Duties, d => d.Name == dutyName);
    }

    [Fact]
    public async void UpdateDuty_ShouldReturnTrueWhenDutyExistInVM()
    {
        // Arrange
        var oldDutyName = "Old Duty Name";
        var duty = new Duty { Name = oldDutyName, Id = 1 }; 
        var newDutyName = "New Duty Name";

        _mockDutyService.Setup(s => s.UpdateDutyInService(newDutyName, It.IsAny<Duty>())).ReturnsAsync(true);
        dutyViewModel.Duties = new System.Collections.ObjectModel.ObservableCollection<Duty>()
        {
            duty
        };

        // Act
        var result = await dutyViewModel.UpdateDuty(duty, newDutyName);

        // Assert
        Assert.True(result);
        _mockDutyService.Verify(s => s.UpdateDutyInService(newDutyName, It.Is<Duty>(d => d.Name == oldDutyName)), Times.Once);
    }
}