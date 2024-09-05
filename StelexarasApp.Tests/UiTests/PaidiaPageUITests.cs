namespace StelexarasApp.Tests.UiTests
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using Xunit;

    namespace StelexarasApp.Tests.UITests
    {
        public class PaidiaPageUITests : IDisposable
        {
            private readonly IWebDriver _driver;

            public PaidiaPageUITests()
            {
                _driver = new ChromeDriver();
            }

            [Fact]
            public void CreatePage_ShouldLoadSuccessfully()
            {
                // Arrange
                _driver.Navigate().GoToUrl("http://localhost:5000/Paidia/Create");

                // Act
                var pageTitle = _driver.Title;

                // Assert
                Assert.Equal("Create Paidia - StelexarasApp", pageTitle);
            }

            [Fact]
            public void IndexPage_ShouldLoadSuccessfully()
            {
                // Arrange
                _driver.Navigate().GoToUrl("http://localhost:5000/Paidia/Index");

                // Act
                var pageTitle = _driver.Title;

                // Assert
                Assert.Equal("Paidia - StelexarasApp", pageTitle);
            }

            [Fact]
            public void CreatePage_ShouldCreateNewPaidia()
            {
                // Arrange
                _driver.Navigate().GoToUrl("http://localhost:5000/Paidia/Create");

                // Act
                _driver.FindElement(By.Id("Name")).SendKeys("Test Paidia");
                _driver.FindElement(By.Id("Description")).SendKeys("Test Description");
                _driver.FindElement(By.CssSelector("input[type='submit']")).Click();

                // Assert
                var successMessage = _driver.FindElement(By.CssSelector(".alert-success")).Text;
                Assert.Contains("has been created", successMessage);
            }

            public void Dispose()
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }
    }


}
