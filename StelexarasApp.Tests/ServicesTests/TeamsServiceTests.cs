using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.Services;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.Tests.ServicesTests
{
    public class TeamsServiceTests
    {
        private readonly TeamsService _peopleService;
        private readonly AppDbContext _dbContext;

        public TeamsServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            _dbContext = new AppDbContext(options);
            _peopleService = new TeamsService(_dbContext);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public async Task AddEkpaideuomenos_ShouldReturnExpectedResult(int id, bool expectedResult)
        {
            var person = GetMockUpEkpaideuomenos(id);

            var result = await _peopleService.AddPaidiInDbAsync(person);
            Assert.Equal(result, expectedResult);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public async Task AddKataskinotis_ShouldReturnExpectedResult(int id, bool expectedResult)
        {
            var person = GetMockUpKataskinotis(id);

            var result = await _peopleService.AddPaidiInDbAsync(person);
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public async Task AddSkini_ShouldFailToAddWhenSkiniNameEmpty()
        {
            var skini = GetMockUpSkini();
            skini.Name = string.Empty;

            var result = await _peopleService.AddSkinesInDb(skini);
            Assert.False(result);
        }

        [Fact]
        public async Task AddSkini_ShouldAddSkiniToDatabase()
        {
            var skini = GetMockUpSkini();

            await _peopleService.AddSkinesInDb(skini);
            var skines = await _dbContext.Skines.ToListAsync();

            Assert.Single(skines);
            Assert.Equal("Skini Test", skines [0].Name);
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

        private Paidi GetMockUpKataskinotis(int id)
        {
            return new Paidi
            {
                Id = id,
                FullName = "Kataskinotis Test",
                Age = 12,
                Skini = GetMockUpSkini(),
                PaidiType = PaidiType.Kataskinotis
            };
        }

        private Paidi GetMockUpEkpaideuomenos(int id)
        {
            return new Paidi
            {
                Id = id,
                FullName = "Ekpaideuomenos Test",
                Age = 16,
                PaidiType = PaidiType.Ekpaideuomenos,
                Skini = GetMockUpSkini()
            };
        }

        private Skini GetMockUpSkini()
        {
            return new Skini
            {
                Id = 3,
                Name = "Skini Test",
                Koinotita = new Koinotita { Id = 1, Name = "Koinotita Test" }
            };
        }
    }
}