using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.Library.Models.Atoma.Staff;
using Moq;
using System.Linq.Expressions;
using StelexarasApp.Library.Models.Domi;
using StelexarasApp.Tests.TestsHelpers;
using StelexarasApp.Library.Models.Atoma;

namespace StelexarasApp.Tests.DataAccessTests;

[Collection("Ignore")]
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

    [Fact(Skip = "Temporarily ignoring this test")]
    public async Task GetStelexosByIdInDb_ShouldReturnStelexos_WhenStelexosExists()
    {
        // Arrange
        var existingOmadarxis = new Omadarxis { Id = 1, FullName = "Test Name", Tel = "123121311", Age = 32, Sex = Sex.Female, Thesi = Thesi.Omadarxis};
        var mockOmadarxisDbSet = SetupMockOmadarxisDbSet([existingOmadarxis]);

        _mockDbContext.Setup(db => db.Omadarxes).Returns(mockOmadarxisDbSet.Object);

        // Act
        var result = await _stelexiRepository.GetStelexosByIdInDb(existingOmadarxis.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingOmadarxis.FullName, result.FullName);
    }

    [Fact(Skip = "Temporarily ignoring this test")]
    public async Task GetStelexosByIdInDb_ShouldReturnNull_WhenStelexosDoesNotExist()
    {
        // Arrange
        var nonExistentId = 999;
        var mockOmadarxisDbSet = SetupMockOmadarxisDbSet([]);
        _mockDbContext.Setup(db => db.Omadarxes).Returns(mockOmadarxisDbSet.Object);

        // Act
        var result = await _stelexiRepository.GetStelexosByIdInDb(nonExistentId);

        // Assert
        Assert.Null(result);
    }

    [Fact(Skip = "Temporarily ignoring this test")]
    public async Task UpdateStelexosInDb_ShouldReturnTrue_WhenStelexosIsUpdated()
    {
        // Arrange
        var existingOmadarxis = new Omadarxis { Id = 1, FullName = "Test Name", Tel = "123121311" };
        var mockOmadarxisDbSet = SetupMockOmadarxisDbSet(new List<Omadarxis> { existingOmadarxis });
        _mockDbContext.Setup(db => db.Omadarxes).Returns(mockOmadarxisDbSet.Object);

        // Act
        existingOmadarxis.FullName = "Updated Name";
        var result = await _stelexiRepository.UpdateStelexosInDb(existingOmadarxis);

        // Assert
        Assert.True(result);
        _mockDbContext.Verify(db => db.Omadarxes.Update(It.IsAny<Omadarxis>()), Times.Once);
        _mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(Skip = "Temporarily ignoring this test")]
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

    [Fact(Skip = "Temporarily ignoring this test")]
    public async Task GetAllKoinotarxesInDb_ShouldReturnAllStelexos()
    {
        // Arrange
        var koinotarxisList = new List<Koinotarxis>
        {
            new() { Id = 1, FullName = "Test Name1", Age = 30, Tel = "1234567890", Koinotita = new Koinotita(), Thesi = Thesi.Koinotarxis, Sex = Sex.Male},
            new() { Id = 2, FullName = "Test Name2", Age = 25, Tel = "0987654321", Koinotita = new Koinotita(), Thesi = Thesi.Koinotarxis, Sex = Sex.Male}
        };

        var mockKoinotarxisDbSet = SetupMockKoinotarxisDbSet(koinotarxisList);
        _mockDbContext.Setup(db => db.Koinotarxes).Returns(mockKoinotarxisDbSet.Object);

        // Act
        var result = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Koinotarxis, string.Empty);

        // Assert
        Assert.NotNull(result);
    }

    private Mock<DbSet<Omadarxis>> SetupMockOmadarxisDbSet(IEnumerable<Omadarxis> omadarxisData)
    {
        var omadarxisQueryable = omadarxisData.AsQueryable();
        var mockOmadarxisDbSet = new Mock<DbSet<Omadarxis>>();

        mockOmadarxisDbSet.As<IQueryable<Omadarxis>>().Setup(m => m.Provider).Returns(omadarxisQueryable.Provider);
        mockOmadarxisDbSet.As<IQueryable<Omadarxis>>().Setup(m => m.Expression).Returns(omadarxisQueryable.Expression);
        mockOmadarxisDbSet.As<IQueryable<Omadarxis>>().Setup(m => m.ElementType).Returns(omadarxisQueryable.ElementType);
        mockOmadarxisDbSet.As<IQueryable<Omadarxis>>().Setup(m => m.GetEnumerator()).Returns(omadarxisQueryable.GetEnumerator());
        mockOmadarxisDbSet.As<IAsyncEnumerable<Omadarxis>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new TestAsyncEnumerator<Omadarxis>(omadarxisData.GetEnumerator()));

        return mockOmadarxisDbSet;
    }


    private Mock<DbSet<Koinotarxis>> SetupMockKoinotarxisDbSet(IEnumerable<Koinotarxis> koinotarxisData)
    {
        var mockKoinotarxisDbSet = new Mock<DbSet<Koinotarxis>>();
        var koinotarxisQueryable = koinotarxisData.AsQueryable();

        var asyncEnumerable = new TestAsyncEnumerable<Koinotarxis>(koinotarxisQueryable);
        mockKoinotarxisDbSet.As<IQueryable<Koinotarxis>>().Setup(m => m.Provider).Returns(koinotarxisQueryable.Provider);
        mockKoinotarxisDbSet.As<IQueryable<Koinotarxis>>().Setup(m => m.Expression).Returns(koinotarxisQueryable.Expression);
        mockKoinotarxisDbSet.As<IQueryable<Koinotarxis>>().Setup(m => m.ElementType).Returns(koinotarxisQueryable.ElementType);
        mockKoinotarxisDbSet.As<IQueryable<Koinotarxis>>().Setup(m => m.GetEnumerator()).Returns(koinotarxisQueryable.GetEnumerator());

        mockKoinotarxisDbSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Expression<Func<Koinotarxis, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Expression<Func<Koinotarxis, bool>> predicate, CancellationToken _) => koinotarxisQueryable.FirstOrDefault(predicate));

        return mockKoinotarxisDbSet;
    }
}