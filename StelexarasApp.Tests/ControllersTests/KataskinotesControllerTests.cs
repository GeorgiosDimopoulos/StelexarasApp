using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.AspNetCore.Mvc;
using StelexarasApp.DataAccess;
using StelexarasApp.Web.Controllers;
using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.Tests.ControllersTests
{
    public class KataskinotisControllerTests
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly PaidiaController _controller;

        public KataskinotisControllerTests()
        {
            _mockContext = new Mock<AppDbContext>();
            _controller = new PaidiaController(_mockContext.Object);
        }

        [Fact]
        public async Task PostKataskinotis_ReturnsCreatedAtAction()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Paidi>>();
            var kataskinotis = new Paidi { Id = 1, FullName= "Test" };

            _mockContext.Setup(m => m.Set<Paidi>()).Returns(mockSet.Object);
            _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var result = await _controller.PostPaidi(kataskinotis);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(_controller.GetPaidi), actionResult.ActionName);
            Assert.Equal(kataskinotis.Id, ((Paidi)actionResult.Value).Id);
        }
    }
}
