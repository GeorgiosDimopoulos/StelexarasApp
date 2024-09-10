using OpenQA.Selenium.Chrome;

namespace StelexarasApp.Tests.UiTests;

public class StelexiPageUITests
{
    [Fact(Skip = "Temporarily ignoring this test")]
    public void CreatePage_ShouldLoadSuccessfully()
    {
        // Arrange
        using var driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://localhost:5000/Stelexi/Create");

        // Act
        var pageTitle = driver.Title;

        // Assert
        Assert.Equal("Create Stelexos - StelexarasApp", pageTitle);

        // Clean up
        driver.Quit();
    }

    [Fact(Skip = "Temporarily ignoring this test")]
    public void IndexPage_ShouldLoadSuccessfully()
    {
        // Arrange
        using var driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://localhost:5000/Stelexi/Index");

        // Act
        var pageTitle = driver.Title;

        // Assert
        Assert.Equal("Teams - StelexarasApp", pageTitle);

        // Clean up
        driver.Quit();
    }
}
