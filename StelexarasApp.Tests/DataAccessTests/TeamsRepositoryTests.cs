using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;

namespace StelexarasApp.Tests.DataAccessTests
{
    public class TeamsRepositoryTests
    {
        private readonly ITeamsRepository _teamsRepository;
        private readonly AppDbContext _dbContext;
        private readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        public TeamsRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: "TestDatabase")
               .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
               .Options;
            _dbContext = new AppDbContext(options);
            _teamsRepository = new TeamsRepository(_dbContext, loggerFactory);
        }

        [Theory]
        [InlineData(3, "TestTeam1", true)]
        [InlineData(-1, "TestTeam3", false)]
        [InlineData(0, "TestTeam4", false)]
        public async Task AddSkini_ShouldReturnExpectedResult(int id, string name, bool expectedResult)
        {
            var team = new Skini
            {
                Id = id,
                Name = name,
                Koinotita = new Koinotita
                {
                    Name = "KoinotitaName"
                }
            };

            var result = await _teamsRepository.AddSkiniInDb(team);

            Assert.Equal(result, expectedResult);
            if (expectedResult)
            {
                var addedTeam = await _teamsRepository.GetSkiniByNameInDb(team.Name);
                Assert.NotNull(addedTeam);
                Assert.Equal(team.Name, addedTeam.Name);
            }
            else
            {
                var addedTeam = await _dbContext.Skines!.FindAsync(id);
                Assert.Null(addedTeam);
            }
        }

        [Theory]
        [InlineData(6, "TestTeam1", true)]
        [InlineData(-1, "TestTeam3", false)]
        public async Task DeleteSkiniInDbAsync_ShouldReturnExpectedResult(int id, string name, bool expectedResult)
        {
            // Arrange
            var team = new Skini
            {
                Id = id,
                Name = name,
                Koinotita = new Koinotita
                {
                    Name = "KoinotitaName"
                }
            };

            await _dbContext.Skines!.AddAsync(team);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _teamsRepository.DeleteSkiniInDb(id);

            // Assert
            Assert.Equal(result, expectedResult);
            if (expectedResult)
            {
                var deletedTeam = await _dbContext.Skines.FindAsync(id);
                Assert.Null(deletedTeam);
            }
            else
            {
                var deletedTeam = await _dbContext.Skines.FindAsync(id);
                Assert.NotNull(deletedTeam);
            }
        }

        [Theory]
        [InlineData(1, "TestTeam1", "TestTeam2", true)]
        public async Task UpdateSkiniInDbAsync_ShouldReturnExpectedResult(int id, string name, string newName, bool expectedResult)
        {
            // Arrange
            var team = new Skini { Id = id, Name = name };
            await _dbContext.Skines!.AddAsync(team);
            await _dbContext.SaveChangesAsync();

            // Act
            team.Name = newName;
            var result = await _teamsRepository.UpdateSkiniInDb(team);

            // Assert
            Assert.Equal(result, expectedResult);
            if (expectedResult)
            {
                var updatedTeam = await _dbContext.Skines.FindAsync(id);
                Assert.NotNull(updatedTeam);
                Assert.Equal(newName, updatedTeam.Name);
            }
            else
            {
                var updatedTeam = await _dbContext.Skines.FindAsync(id);
                Assert.NotNull(updatedTeam);
                Assert.NotEqual(newName, updatedTeam.Name);
            }
        }

        [Fact]
        public async Task GetSkines_ShouldReturnTeams()
        {
            // Arrange
            var teams = new List<Skini>
            {
                new()
                {
                    Id = 1, Name = "TestTeam1", Paidia = new List<Paidi>(), Koinotita = new Koinotita
                    {
                        Name = "KoinotitaName"
                    }
                },
                new() {
                    Id = 2, Name = "TestTeam2", Paidia = new List<Paidi>(), Koinotita = new Koinotita
                    {
                        Name = "KoinotitaName2"
                    }
                }
            };
            await _dbContext.Skines!.AddRangeAsync(teams);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _teamsRepository.GetSkinesInDb();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetSkiniById_ShouldReturnTeam()
        {
            // Arrange
            var team = new Skini { Id = 1, Name = "TestTeam" };
            await _dbContext.Skines!.AddAsync(team);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _teamsRepository.GetSkiniByNameInDb(team.Name);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(team.Id, result.Id);
        }

        [Fact]
        public async Task GetKoinotitaByName_ShouldReturnTeam()
        {
            // Arrange
            var koinotita = new Koinotita { Id = 1, Name = "TestKOinotita" };
            await _dbContext.Koinotites!.AddAsync(koinotita);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _teamsRepository.GetKoinotitaByNameInDb(koinotita.Name);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(koinotita.Id, result.Id);
        }

        [Theory]
        [InlineData(1, "Updated Name", true)]
        [InlineData(2, null, false)]
        public async Task UpdateKoinotitaInDbAsync_ShouldReturnExpectedResult(int id, string newName, bool expectedResult)
        {
            // Arrange
            var koinotita = new Koinotita { Id = 1, Name = "TestKOinotita" };
            await _dbContext.Koinotites!.AddAsync(koinotita);
            await _dbContext.SaveChangesAsync();

            if (newName != null)
                koinotita.Name = newName;
            else
                koinotita = null;

            // Act
            var result = await _teamsRepository.UpdateKoinotitaInDb(koinotita!);

            // Assert
            Assert.Equal(result, expectedResult);
            if (expectedResult)
            {
                var updatedKoinotita = await _dbContext.Koinotites.FindAsync(id);
                Assert.NotNull(updatedKoinotita);
                Assert.Equal(newName, updatedKoinotita.Name);
            }
        }

        [Fact]
        public async Task GetTomeaByName_ShouldReturnTeam()
        {
            // Arrange
            var tomeas = new Tomeas { Id = 1, Name = "TestTomeas" };
            await _dbContext.Tomeis!.AddAsync(tomeas);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _teamsRepository.GetTomeaByNameInDb(tomeas.Name);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tomeas.Id, result.Id);
        }

        [Theory]
        [InlineData(1, "Updated Name", true)]
        [InlineData(2, null, false)]
        public async Task UpdateTomeasInDbAsync_ShouldReturnExpectedResult(int id, string newName, bool expectedResult)
        {
            // Arrange
            var tomeas = new Tomeas
            {
                Id = 1,
                Name = "TestTomeas"
            };
            await _dbContext.Tomeis!.AddAsync(tomeas);
            await _dbContext.SaveChangesAsync();

            if (newName != null)
                tomeas.Name = newName;
            else
                tomeas = null;

            // Act
            var result = await _teamsRepository.UpdateTomeasInDb(tomeas!);

            // Assert
            Assert.Equal(expectedResult, result);
            if (expectedResult)
            {
                var updatedTomeas = await _dbContext.Tomeis.FindAsync(id);
                Assert.NotNull(updatedTomeas);
                Assert.Equal(newName, updatedTomeas.Name);
            }
        }

        [Fact]
        public async Task DeleteKoinotitaInDbAsync_ShouldReturnExpectedResult()
        {
            // Arrange
            var koinotita = new Koinotita { Id = 1, Name = "TestKOinotita" };
            await _dbContext.Koinotites!.AddAsync(koinotita);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _teamsRepository.DeleteKoinotitaInDb(koinotita.Id);

            // Assert
            Assert.True(result);
            var deletedKoinotita = await _dbContext.Koinotites.FindAsync(koinotita.Id);
            Assert.Null(deletedKoinotita);
        }

        [Fact]
        public async Task DeleteTomeasInDbAsync_ShouldReturnExpectedResult()
        {
            // Arrange
            var tomeas = new Tomeas
            {
                Id = 1,
                Name = "TestTomeas",
                Koinotites = new List<Koinotita>(),
                Tomearxis = new Tomearxis()
            };

            await _dbContext.Tomeis!.AddAsync(tomeas);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _teamsRepository.DeleteTomeasInDb(tomeas.Id);

            // Assert
            Assert.True(result);
            var deletedTomeas = await _dbContext.Tomeis.FindAsync(tomeas.Id);
            Assert.Null(deletedTomeas);
        }

        [Fact]
        public async Task GetTomeaByName_ShouldReturnTomea()
        {
            // Arrange
            await _dbContext.Tomearxes!.AddAsync(GetTomeas().Tomearxis);
            await _dbContext.SaveChangesAsync();

            await _dbContext.Tomeis!.AddAsync(GetTomeas());
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _teamsRepository.GetTomeaByNameInDb(GetTomeas().Name);

            // Assert
            Assert.NotNull(result);
        }

        private Tomeas GetTomeas()
        {
            return new Tomeas
            {
                Id = 1,
                Name = "TestTomeas",
                Tomearxis = new Tomearxis
                {
                    Id = 1,
                    FullName = "Test Tomearxis",
                },
                Koinotites = new List<Koinotita>()
            };
        }
    }
}