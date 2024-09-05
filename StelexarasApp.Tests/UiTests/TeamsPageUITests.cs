using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace StelexarasApp.Tests.UiTests
{   
    namespace StelexarasApp.Tests.UITests
    {
        public class TeamsPageUITests
        {
            [Fact]
            public void CreatePage_ShouldLoadSuccessfully()
            {
                // Arrange
                using var driver = new ChromeDriver();
                driver.Navigate().GoToUrl("http://localhost:5000/Teams/Create");

                // Act
                var pageTitle = driver.Title;

                // Assert
                Assert.Equal("Create Team - StelexarasApp", pageTitle);

                // Clean up
                driver.Quit();
            }

            [Fact]
            public void IndexPage_ShouldLoadSuccessfully()
            {
                // Arrange
                using var driver = new ChromeDriver();
                driver.Navigate().GoToUrl("http://localhost:5000/Teams/Index");

                // Act
                var pageTitle = driver.Title;

                // Assert
                Assert.Equal("Teams - StelexarasApp", pageTitle);

                // Clean up
                driver.Quit();
            }
        }
    }

}
