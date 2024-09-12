using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace StelexarasApp.DataAccess.Helpers;
public class DbHealthCheck : IHealthCheck
{
    private readonly AppDbContext _dbContext;

    public DbHealthCheck(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var canConnect = await _dbContext.Database.CanConnectAsync(cancellationToken);
            if (canConnect)
            {
                return HealthCheckResult.Healthy("Database is reachable.");
            }
            return HealthCheckResult.Unhealthy("Cannot reach database.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy($"Exception occurred: {ex.Message}");
        }
    }
}
