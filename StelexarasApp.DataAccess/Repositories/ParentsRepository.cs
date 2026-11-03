using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StelexarasApp.Library.Models.Atoma;

namespace StelexarasApp.DataAccess.Repositories;

public class ParentsRepository(AppDbContext dbContext, ILoggerFactory loggerFactory) : IParentsRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly ILogger<PaidiaRepository> _logger = loggerFactory.CreateLogger<PaidiaRepository>();

    public async Task<IEnumerable<Parent>> GetParentsInDb(ParentQueryParameters? queryParameters)
    {
        return await _dbContext.Parents.ToListAsync();
    }

    public async Task<Parent> GetParentByIdInDb(int id, ParentQueryParameters? parentQueryParameters)
    {
        var query = _dbContext.Parents.Include(p => p.Paidia);
        return await query.FirstOrDefaultAsync(p => p.Id == id) ?? throw new KeyNotFoundException($"Parent with Id {id} not found.");
    }

    public async Task<Parent> GetParentByChildIdInDb(int childId, ParentQueryParameters? parentQueryParameters)
    {
        var paidi = await _dbContext.Paidia.FirstOrDefaultAsync(p => p.Id == childId);
        if (paidi == null)
            throw new KeyNotFoundException($"Paidi with Id {childId} not found.");
        return paidi.Parent ?? throw new KeyNotFoundException($"Parent with Child Id {childId} not found.");
    }

    public async Task<Parent> GetParentByNameInDb(string name, ParentQueryParameters? parentQueryParameters)
    {
        var parent = await _dbContext.Parents.FirstOrDefaultAsync(p => p.FirstName == name || p.LastName == name);
        if (parent == null)
            throw new KeyNotFoundException($"Parent with Name {name} not found.");
        return parent;
    }

    public async Task<bool> AddParentInDb(Parent parent)
    {
        var existingParent = await _dbContext.Parents.FirstOrDefaultAsync(p => p.Phone.Equals(parent.Phone));
        if (existingParent != null)
        {
            _logger.LogWarning("Parent with the same ID already exists.");
            return false;
        }

        await _dbContext.Parents.AddAsync(parent);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteParentInDb(int id)
    {
        var parent = await _dbContext.Parents.FindAsync(id);
        if (parent == null)
        {
            _logger.LogWarning("Parent with the given Id doesn't exist.");
            return false;
        }
        _dbContext.Parents.Remove(parent);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateParentInDb(int id, Parent parent)
    {
        var existingParent = await _dbContext.Parents.FindAsync(id);
        if (existingParent == null)
        {
            _logger.LogWarning("Parent with the given Id doesn't exist.");
            return false;
        }

        _dbContext.Entry(existingParent).CurrentValues.SetValues(parent);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
