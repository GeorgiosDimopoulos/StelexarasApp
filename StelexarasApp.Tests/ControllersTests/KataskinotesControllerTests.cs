using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.AspNetCore.Mvc;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Atoma.Paidia;
using StelexarasApp.Web.Controllers;

namespace StelexarasApp.Tests.ControllersTests
{
    public class KataskinotisControllerTests
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly KataskinotesController _controller;

        public KataskinotisControllerTests()
        {
            _mockContext = new Mock<AppDbContext>();
            _controller = new KataskinotesController(_mockContext.Object);
        }

        [Fact]
        public async Task PostKataskinotis_ReturnsCreatedAtAction()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Kataskinotis>>();
            var kataskinotis = new Kataskinotis { Id = 1, FullName= "Test" };

            _mockContext.Setup(m => m.Set<Kataskinotis>()).Returns(mockSet.Object);
            _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var result = await _controller.PostKataskinotis(kataskinotis);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(_controller.GetKataskinotis), actionResult.ActionName);
            Assert.Equal(kataskinotis.Id, ((Kataskinotis)actionResult.Value).Id);
        }
    }
}
