using Moq;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.IServices;
using StelexarasApp.ViewModels;

namespace StelexarasApp.Tests.ViewModelsTests
{
    public class StaffViewModelTests
    {
        private readonly Mock<IStaffService> _mockPaidiaService;
        private readonly StaffViewModel staffViewModel;

        public StaffViewModelTests()
        {
            _mockPaidiaService = new Mock<IStaffService>();
            staffViewModel = new StaffViewModel(_mockPaidiaService.Object, "Ekpaideutis");
        }

        [Fact]
        public void GetStelexosById_WhenCalled_ReturnsStelexos()
        {
            // Arrange
            var stelexoi = GetMockUpStelexoi();
            _mockPaidiaService.Setup(x => x.GetStelexoiAnaThesiInService(Thesi.Omadarxis)).ReturnsAsync(stelexoi);

            // Act
            var result = staffViewModel.GetStelexoi(Thesi.Omadarxis);

            // Assert
            Assert.NotNull(result);
            _mockPaidiaService.Verify(service => service.GetStelexoiAnaThesiInService(Thesi.Omadarxis), Times.Once);
        }

        private IEnumerable<StelexosDto> GetMockUpStelexoi()
        {
            return
            [
                new()
                {
                    Id = 1,
                    FullName = "Some Name",
                    Thesi = Thesi.Ekpaideutis,
                    XwrosName = "SomeXwros",
                    Sex = Sex.Male,
                    Age = 30,
                }
            ];
        }
    }
}
