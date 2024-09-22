using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using Moq;
using System.Linq.Expressions;
using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.Tests.DataAccessTests;

public class StaffRepositoryTests
{
    private readonly IStaffRepository _stelexiRepository;
    private readonly Mock<AppDbContext> _mockDbContext;
    private readonly Mock<ILoggerFactory> _mockLoggerFactory;

    public StaffRepositoryTests()
    {
        _mockDbContext = new Mock<AppDbContext>();
        _mockLoggerFactory = new Mock<ILoggerFactory>();

        var mockOmadarxisDbSet = new Mock<DbSet<Omadarxis>>();
        _mockDbContext.Setup(db => db.Omadarxes).Returns(mockOmadarxisDbSet.Object);
        var mockKoinotarxisDbSet = new Mock<DbSet<Koinotarxis>>();
        _mockDbContext.Setup(db => db.Koinotarxes).Returns(mockKoinotarxisDbSet.Object);
        var mockTomearxisDbSet = new Mock<DbSet<Tomearxis>>();
        _mockDbContext.Setup(db => db.Tomearxes).Returns(mockTomearxisDbSet.Object);

        _stelexiRepository = new StaffRepository(_mockDbContext.Object, _mockLoggerFactory.Object);
    }

    public async Task GetStelexosByIdInDb_ShouldReturnStelexos_WhenStelexosExists()
    {
        // Arrange
        var existingOmadarxis = new Omadarxis { Id = 1, FullName = "Test Name", Tel = "123121311" };
        var mockOmadarxisDbSet = SetupMockOmadarxisDbSet(new List<Omadarxis> { existingOmadarxis });

        _mockDbContext.Setup(db => db.Omadarxes).Returns(mockOmadarxisDbSet.Object);

        // Act
        var result = await _stelexiRepository.GetStelexosByIdInDb(existingOmadarxis.Id, existingOmadarxis.Thesi);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingOmadarxis.FullName, result.FullName);
    }

    public async Task GetStelexosByIdInDb_ShouldReturnNull_WhenStelexosDoesNotExist()
    {
        // Arrange
        var nonExistentId = 999;
        /HERE
        var mockOmadarxisDbSet = SetupMockOmadarxisDbSet(new List<Omadarxis>());
        _mockDbContext.Setup(db => db.Omadarxes).Returns(mockOmadarxisDbSet.Object);

        // Act
        var result = await _stelexiRepository.GetStelexosByIdInDb(nonExistentId, Thesi.Omadarxis);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddStelexosInDb_ShouldReturnTrue_WhenStelexosIsAdded()
    {
        // Arrange
        var stelexos = new Omadarxis
        {
            Id = 1,
            Thesi = Thesi.Omadarxis,
            FullName = "Test Name",
            Tel = "123-456-7890"
        };

        // Act
        var result = await _stelexiRepository.AddStelexosInDb(stelexos);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task AddStelexosInDb_ShouldThrowException_WhenRequiredPropertiesAreMissing()
    {
        // Arrange
        var stelexos = new Omadarxis
        {
            Id = 1,
            Thesi = Thesi.Omadarxis,
            FullName = "TestName",
        };

        // Act & Assert
        await Assert.ThrowsAsync<DbUpdateException>(async () =>
        {
            await _stelexiRepository.AddStelexosInDb(stelexos);
        });
    }

    [Fact]
    public async Task UpdateStelexosInDb_ShouldReturnTrue_WhenStelexosIsUpdated()
    {
        // Arrange
        var existingOmadarxis = new Omadarxis { Id = 1, FullName = "Test Name", Tel = "123121311" };
        var mockOmadarxisDbSet = SetupMockOmadarxisDbSet(new List<Omadarxis> { existingOmadarxis });
        _mockDbContext.Setup(db => db.Omadarxes).Returns(mockOmadarxisDbSet.Object);

        // Act
        var result = await _stelexiRepository.UpdateStelexosInDb(existingOmadarxis);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteStelexosInDb_ShouldReturnTrue_WhenStelexosIsDeleted()
    {
        // Arrange
        var existingOmadarxis = new Omadarxis { Id = 1, FullName = "Test Name", Tel = "123-456-7890" };
        var mockOmadarxisDbSet = SetupMockOmadarxisDbSet(new List<Omadarxis>
        {
            existingOmadarxis
        });

        _mockDbContext.Setup(db => db.Omadarxes).Returns(mockOmadarxisDbSet.Object);

        // Act
        var result = await _stelexiRepository.DeleteStelexosInDb(existingOmadarxis.Id, existingOmadarxis.Thesi);

        // Assert
        Assert.True(result);
        _mockDbContext.Verify(db => db.Omadarxes.Remove(It.IsAny<Omadarxis>()), Times.Once);
        _mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetAllOmadarxesInDb_ShouldReturnAllStelexos()
    {
        // Arrange
        var existingOmadarxis = new Omadarxis { Id = 1, FullName = "Test Name", Tel = "123-456-7890", Thesi = Thesi.Omadarxis };
        var mockOmadarxisDbSet = SetupMockOmadarxisDbSet(new List<Omadarxis>
        {
            existingOmadarxis
        });
        _mockDbContext.Setup(db => db.Omadarxes).Returns(mockOmadarxisDbSet.Object);

        // Act
        var result = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, "");

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetAllKoinotarxesInDb_ShouldReturnAllStelexos()
    {
        // Arrange
        var omadarxisList = new List<Koinotarxis>
        {
            new Koinotarxis { Id = 1, FullName = "Test Name 1", Age = 30, Tel = "1234567890", Koinotita = new Koinotita(), Thesi = Thesi.Koinotarxis},
            new Koinotarxis { Id = 2, FullName = "Test Name 2", Age = 25, Tel = "0987654321", Koinotita = new Koinotita(), Thesi = Thesi.Koinotarxis}
        };

        var mockKoinotarxisDbSet = SetupMockKoinotarxisDbSet(omadarxisList);
        _mockDbContext.Setup(db => db.Koinotarxes).Returns(mockKoinotarxisDbSet.Object);

        // Act
        var result = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Koinotarxis, string.Empty);

        // Assert
        Assert.NotNull(result);
    }

    private Mock<DbSet<Omadarxis>> SetupMockOmadarxisDbSet(IEnumerable<Omadarxis> omadarxisData)
    {
        var mockOmadarxisDbSet = new Mock<DbSet<Omadarxis>>();
        var omadarxisQueryable = omadarxisData.AsQueryable();

        mockOmadarxisDbSet.As<IQueryable<Omadarxis>>().Setup(m => m.Provider).Returns(omadarxisQueryable.Provider);
        mockOmadarxisDbSet.As<IQueryable<Omadarxis>>().Setup(m => m.Expression).Returns(omadarxisQueryable.Expression);
        mockOmadarxisDbSet.As<IQueryable<Omadarxis>>().Setup(m => m.ElementType).Returns(omadarxisQueryable.ElementType);
        mockOmadarxisDbSet.As<IQueryable<Omadarxis>>().Setup(m => m.GetEnumerator()).Returns(omadarxisQueryable.GetEnumerator());

        mockOmadarxisDbSet.Setup(m => m.ToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(omadarxisData.ToList());

        mockOmadarxisDbSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Expression<Func<Omadarxis, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Expression<Func<Omadarxis, bool>> predicate, CancellationToken _) => omadarxisQueryable.FirstOrDefault(predicate));


        return mockOmadarxisDbSet;
    }

    private Mock<DbSet<Koinotarxis>> SetupMockKoinotarxisDbSet(IEnumerable<Koinotarxis> omadarxisData)
    {
        var mockKoinotarxisDbSet = new Mock<DbSet<Koinotarxis>>();
        var koinotarxisQueryable = omadarxisData.AsQueryable();

        mockKoinotarxisDbSet.As<IQueryable<Koinotarxis>>().Setup(m => m.Provider).Returns(koinotarxisQueryable.Provider);
        mockKoinotarxisDbSet.As<IQueryable<Koinotarxis>>().Setup(m => m.Expression).Returns(koinotarxisQueryable.Expression);
        mockKoinotarxisDbSet.As<IQueryable<Koinotarxis>>().Setup(m => m.ElementType).Returns(koinotarxisQueryable.ElementType);
        mockKoinotarxisDbSet.As<IQueryable<Koinotarxis>>().Setup(m => m.GetEnumerator()).Returns(koinotarxisQueryable.GetEnumerator());

        return mockKoinotarxisDbSet;
    }
}