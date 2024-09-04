using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.DataAccess.Models.Domi;
using Microsoft.Extensions.Logging;
using Moq;

namespace StelexarasApp.Tests.DataAccessTests
{
    public class PaidiaRepositoryTests
    {
        private readonly IPaidiRepository _paidiRepository;
        private readonly AppDbContext _dbContext;
        private readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        
        public PaidiaRepositoryTests()
        {
            var mockLogger = new Mock<ILogger<PaidiRepository>>();
            var mockLoggerFactory = new Mock<ILoggerFactory>();
            mockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(mockLogger.Object);
            _dbContext = new Mock<AppDbContext>().Object;

            // var options = new DbContextOptionsBuilder<AppDbContext>()
            //    .UseInMemoryDatabase(databaseName: "TestDatabase")
            //    .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            //    .Options;
            _paidiRepository = new PaidiRepository(_dbContext, mockLoggerFactory.Object);
        }

        [Theory]
        [InlineData(3, PaidiType.Kataskinotis, true)]
        [InlineData(2, PaidiType.Ekpaideuomenos, true)]
        [InlineData(-1, PaidiType.Ekpaideuomenos, false)]
        [InlineData(0, PaidiType.Kataskinotis, false)]
        public async Task AddPaidi_ShouldReturnExpectedResult(int id, PaidiType paidiType, bool expectedResult)
        {
            var paidi = new Paidi { Id = id, FullName = "Test", Age = 10, PaidiType = paidiType };
            var result = await _paidiRepository.AddPaidiInDb(paidi);
            Assert.Equal(result, expectedResult);
        }

        [Theory]
        [InlineData(6, PaidiType.Kataskinotis, true)]
        [InlineData(7, PaidiType.Ekpaideuomenos, true)]
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
            var result = await _paidiRepository.DeletePaidiInDb(paidi);

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
        [InlineData(9999, -2, false)]
        public async Task MovePaidiToNewSkini_ShouldReturnExpectedResult(int paidiId, int newSkiniId, bool expectedResult)
        {
            // Arrange
            if (paidiId == 1)
            {
                var koinotita = new Koinotita 
                {
                    Id = 1,
                    Name = "Ipiros",
                    Skines = new List <Skini>()
                };

                var existingSkini = new Skini {
                    Id = 4, Name = "Pindos",
                    Koinotita = koinotita,
                    Paidia = new List<Paidi>()
                };

                var newSkini = new Skini
                {
                    Id = newSkiniId,
                    Name = "NewSkini",
                    Koinotita = koinotita,
                    Paidia = new List<Paidi>()
                };

                await _paidiRepository.AddSkinesInDb(existingSkini);
                await _paidiRepository.AddSkinesInDb(newSkini);

                var existingPaidi = new Paidi
                {
                    Id = paidiId,
                    FullName = "Test Paidi",
                    Age = 15,
                    Sex = Sex.Male,
                    Skini = existingSkini,
                    PaidiType = PaidiType.Kataskinotis,
                };
                
                await _paidiRepository.AddPaidiInDb(existingPaidi);
            }

            await _dbContext.SaveChangesAsync();
            var result = await _paidiRepository.MovePaidiToNewSkiniInDb(paidiId, newSkiniId);

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
