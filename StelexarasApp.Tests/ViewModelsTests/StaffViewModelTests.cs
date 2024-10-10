using Moq;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.ViewModels.PeopleViewModels;

namespace StelexarasApp.Tests.ViewModelsTests
{
    public class StaffViewModelTests
    {
        private readonly Mock<IStaffService> _mockPaidiaService;
        private readonly StaffViewModel staffViewModel;

        public StaffViewModelTests()
        {
            _mockPaidiaService = new Mock<IStaffService>();
            staffViewModel = new StaffViewModel(_mockPaidiaService.Object);
        }

        //[Fact]
        //public void GetStelexosById_WhenCalled_ReturnsStelexos()
        //{
        //    // Arrange
        //    var stelexoi = GetMockUpStelexoi();
        //    _mockPaidiaService.Setup(x => x.GetStelexoiAnaThesiInService(Thesi.Omadarxis)).ReturnsAsync(stelexoi);

        //    // Act
        //    var result = staffViewModel.GetStelexoiAnaThesi(Thesi.Omadarxis);

        //    // Assert
        //    Assert.NotNull(result);
        //    _mockPaidiaService.Verify(service => service.GetStelexoiAnaThesiInService(Thesi.Omadarxis), Times.Once);
        //}

        private IStelexosDto GetMockUpStelexos(Thesi? thesi = Thesi.Omadarxis, string name = "Some name", string xwrosName = "someXwros")
        {
            return thesi switch
            {
                Thesi.Omadarxis => new OmadarxisDto
                {
                    Age = 20,
                    FullName = name,
                    Sex = Sex.Female,
                    DtoXwrosName = xwrosName,
                    Thesi = thesi ?? Thesi.None,
                },
                Thesi.Koinotarxis => new KoinotarxisDto
                {
                    Age = 20,
                    FullName = name,
                    Sex = Sex.Female,
                    DtoXwrosName = xwrosName,
                    Thesi = thesi ?? Thesi.None,
                },
                Thesi.Tomearxis => new TomearxisDto
                {
                    Age = 20,
                    FullName = name,
                    Sex = Sex.Female,
                    DtoXwrosName = xwrosName,
                    Thesi = thesi ?? Thesi.None,
                },
                _ => throw new ArgumentException("Invalid Thesi value", nameof(thesi)),
            };
        }

    }
}
