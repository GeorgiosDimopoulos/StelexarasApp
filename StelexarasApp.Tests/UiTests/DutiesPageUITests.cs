namespace StelexarasApp.Tests.UiTests
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using Xunit;

    namespace StelexarasApp.Tests.UITests
    {
        public class DutiesPageUITests : IDisposable
        {
            private readonly IWebDriver _driver;

            public DutiesPageUITests()
            {
                _driver = new ChromeDriver();
            }

            [Fact(Skip = "Temporarily ignoring this test")]
            public void CreatePage_ShouldLoadSuccessfully()
            {
                // Arrange
                _driver.Navigate().GoToUrl("http://localhost:5000/Duties/Create");

                // Act
                var pageTitle = _driver.Title;

                // Assert
                Assert.Equal("Create Duties - StelexarasApp", pageTitle);
            }

            [Fact(Skip = "Temporarily ignoring this test")]
            public void IndexPage_ShouldLoadSuccessfully()
            {
                // Arrange
                _driver.Navigate().GoToUrl("http://localhost:5000/Duties/Index");

                // Act
                var pageTitle = _driver.Title;

                // Assert
                Assert.Equal("Duties - StelexarasApp", pageTitle);
            }

            [Fact(Skip = "Temporarily ignoring this test")]
            public void CreatePage_ShouldCreateNewDuty()
            {
                // Arrange
                _driver.Navigate().GoToUrl("http://localhost:5000/Duties/Create");

                // Act
                _driver.FindElement(By.Id("Name")).SendKeys("Test Duties");
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
