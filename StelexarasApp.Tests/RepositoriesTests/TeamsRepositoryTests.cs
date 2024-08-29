using StelexarasApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess.Models.Atoma;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.Tests.ServicesTests
{
    public class TeamsServiceTests
    {
        private readonly AppDbContext _dbContext;
        private readonly IPaidiRepository _paidiRepository;

        public TeamsServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            _dbContext = new AppDbContext(options);
            _paidiRepository = new PaidiRepository(_dbContext);
        }

        [Fact]
        public async Task AddPaidiAsync_ShouldAddPaidiToDatabase()
        {
            // Arrange
            var paidi = new Paidi { Id = 1, FullName = "Test Paidi" };

            // Act
            await _paidiRepository.AddPaidiInDbAsync(paidi);
            var result = await _dbContext.Paidia.FindAsync(paidi.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(paidi.FullName, result.FullName);
        }

        [Fact]
        public async Task DeletePaidiAsync_ShouldDeletePaidiFromDatabase()
        {
            // Arrange
            var paidi = new Paidi { Id = 1, FullName = "Test Paidi" };
            await _dbContext.Paidia.AddAsync(paidi);
            await _dbContext.SaveChangesAsync();

            // Act
            await _paidiRepository.DeletePaidiInDb(paidi);
            var result = await _dbContext.Paidia.FindAsync(paidi.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetPaidiById_ShouldReturnPaidiFromDatabase()
        {
            // Arrange
            var paidi = new Paidi { Id = 1, FullName = "Test Paidi" };
            await _dbContext.Paidia.AddAsync(paidi);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _paidiRepository.GetPaidiById(paidi.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(paidi.FullName, result.FullName);
        }

        [Fact]
        public async Task GetPaidia_ShouldReturnAllPaidiaFromDatabase()
        {
            // Arrange
            var paidi1 = new Paidi { Id = 1, FullName = "Test Paidi 1" };
            var paidi2 = new Paidi { Id = 2, FullName = "Test Paidi 2" };
            await _dbContext.Paidia.AddRangeAsync(paidi1, paidi2);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _paidiRepository.GetPaidia(PaidiType.Kataskinotis);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}