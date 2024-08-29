using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.DtosModels;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.Services;

namespace StelexarasApp.Tests.ServicesTests
{
    public class PaidiaServiceTests
    {
        private readonly PaidiaService _paidiaService;
        private readonly IPaidiRepository _paidiRepository;
        private readonly IMapper _mapper; 
        private readonly AppDbContext _dbContext;

        public PaidiaServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            _paidiaService = new PaidiaService(_paidiRepository, _mapper);
        }

        [Theory]
        [InlineData(1, PaidiType.Kataskinotis, true)]
        [InlineData(1, PaidiType.Ekpaideuomenos, false)]
        [InlineData(-1, PaidiType.Ekpaideuomenos, false)]
        [InlineData(0, PaidiType.Ekpaideuomenos, false)]
        public async Task AddPaidi_ShouldReturnExpectedResult(int id, PaidiType paidiType, bool expectedResult)
        {
            var paidi = new DataAccess.DtosModels.PaidiDto { Id = id, FullName = "Test", Age = 10, PaidiType = paidiType };
            var result = await _paidiaService.AddPaidiInDbAsync(paidi);
            Assert.Equal(result, expectedResult);
        }

        //[Theory]
        //[InlineData("SkiniTest", true)]
        //[InlineData("", false)]
        //public async Task AddSkini_ShouldReturnExpectedResult(string skiniName, bool expectedResult)
        //{
        //    var skini = new Skini { Name = skiniName };
        //    var result = await _paidiaService.AddSkinesInDb(skini);

        //    Assert.Equal(result, expectedResult);

        //    if (expectedResult)
        //    {
        //        var skines = await _paidiaService.Skines.ToListAsync();
        //        Assert.Single(skines);
        //    }
        //}

        [Theory]
        [InlineData(1, PaidiType.Kataskinotis, true)]
        [InlineData(2, PaidiType.Ekpaideuomenos, true)]
        [InlineData(-1, PaidiType.Kataskinotis, false)]
        [InlineData(0, PaidiType.Ekpaideuomenos, false)]
        public async Task DeletePaidiInDbAsync_ShouldReturnExpectedResult(int id, PaidiType paidiType, bool expectedResult)
        {
            // Arrange
            Paidi paidi = new Paidi { Id = id, FullName = "Test", Age = 10, PaidiType = paidiType };
            if (id > 0)
            {
                _dbContext.Paidia.Add(paidi);
                await _dbContext.SaveChangesAsync();
            }

            // Act
            var result = await _paidiaService.DeletePaidiInDb(paidi.Id);

            // Assert
            Assert.Equal(expectedResult, result);

            if (expectedResult)
            {
                var deletedPaidi = await _dbContext.Paidia.FindAsync(id);
                Assert.Null(deletedPaidi);
            }
        }

        [Theory]
        [InlineData(1, 2, true)]
        [InlineData(1, 9999, false)]
        [InlineData(9999, 2, false)]
        [InlineData(9999, 9999, false)]
        public async Task MovePaidiToNewSkini_ShouldReturnExpectedResult(int paidiId, int newSkiniId, bool expectedResult)
        {
            // Arrange
            if (paidiId == 1)
            {
                var existingPaidi = new PaidiDto
                {
                    Id = paidiId,
                    FullName = "Test Paidi",
                    Age = 15,
                    Sex = Sex.Male,
                    PaidiType = PaidiType.Kataskinotis,
                    SkiniName = "Old Skini"
                };

                // await _paidiaService.AddSkinesInDb(existingPaidi.Skini);
                await _paidiaService.AddPaidiInDbAsync(existingPaidi);
            }

            // await _dbContext.SaveChangesAsync();
            var result = await _paidiaService.MovePaidiToNewSkini(paidiId, newSkiniId);

            Assert.Equal(expectedResult, result);

            if (expectedResult)
            {
                var movedPaidi = await _dbContext.Paidia
                    .Include(p => p.Skini)
                    .FirstOrDefaultAsync(p => p.Id == paidiId);

                Assert.NotNull(movedPaidi);
                Assert.Equal(newSkiniId, movedPaidi.Skini.Id);
            }
        }
    }
}
