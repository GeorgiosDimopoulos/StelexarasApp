using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.Services;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess.Models.Atoma;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace StelexarasApp.Tests.ServicesTests
{
    public class TeamsServiceTests
    {
        private readonly TeamsService _peopleService;
        private readonly AppDbContext _dbContext;
        private readonly Mock<ILogger<TeamsService>> _loggerMock;

        public TeamsServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            // _dbContext = new Mock<AppDbContext>(options);
            _dbContext = new AppDbContext(options);
            _loggerMock = new Mock<ILogger<TeamsService>>();
            _peopleService = new TeamsService(_dbContext, _loggerMock.Object);
        }

        [Theory]
        [InlineData(1, PaidiType.Kataskinotis, true)]
        [InlineData(1, PaidiType.Ekpaideuomenos, false)]
        [InlineData(-1, PaidiType.Ekpaideuomenos, false)]
        [InlineData(0, PaidiType.Ekpaideuomenos, false)]
        public async Task AddPaidi_ShouldReturnExpectedResult(int id, PaidiType paidiType, bool expectedResult)
        {
            Paidi paidi = new Paidi { Id = id, FullName = "Test", Age = 10, PaidiType = paidiType };
            var result = await _peopleService.AddPaidiInDbAsync(paidi);
            Assert.Equal(result, expectedResult);
        }

        [Theory]
        [InlineData("SkiniTest", true)]
        [InlineData("", false)]
        public async Task AddSkini_ShouldReturnExpectedResult(string skiniName, bool expectedResult)
        {
            var skini = new Skini { Name = skiniName };
            var result = await _peopleService.AddSkinesInDb(skini);

            Assert.Equal(result, expectedResult);

            if (expectedResult)
            {
                var skines = await _dbContext.Skines.ToListAsync();
                Assert.Single(skines);
            }
        }

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
            var result = await _peopleService.DeletePaidiInDb(paidi);

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
                var existingPaidi = new Paidi
                {
                    Id = paidiId,
                    FullName = "Test Paidi",
                    Age = 15,
                    Sex = Sex.Male,
                    PaidiType = PaidiType.Kataskinotis,
                    Skini = new Skini { Id = 1, Name = "Old Skini" }
                };

                await _peopleService.AddPaidiInDbAsync(existingPaidi);
                await _peopleService.AddSkinesInDb(existingPaidi.Skini);
            }

            if (newSkiniId == 2)
            {
                var newSkini = new Skini
                {
                    Id = newSkiniId,
                    Name = "New Skini"
                };
                await _peopleService.AddSkinesInDb(newSkini);
            }
            // await _dbContext.SaveChangesAsync();
            var result = await _peopleService.MovePaidiToNewSkini(paidiId, newSkiniId);

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