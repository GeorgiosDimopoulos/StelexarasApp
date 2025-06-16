using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace StelexarasApp.API.Helpers;

public static class HealthCheck
{
    public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration ["ConnectionStrings:DefaultConnection"];
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString), "The connection string 'ConnectionStrings:DefaultConnection' cannot be null or empty.");
        }

        services.AddHealthChecks()
                .AddSqlServer(connectionString,
                          healthQuery: "select 1",
                          name: "SQL Server",
                          failureStatus: HealthStatus.Unhealthy,
                          tags: ["Feedback", "Database"])
                .AddCheck<DbHealthCheck>(
                    name: "Database Health Check",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: ["EF", "Database"]);

        services.AddHealthChecksUI(opt =>
        {
            opt.SetEvaluationTimeInSeconds(10); //time in seconds between check
            opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks
            opt.SetApiMaxActiveRequests(1); //api requests concurrency
            opt.AddHealthCheckEndpoint("feedback api", "/health");
        }).AddInMemoryStorage();

        services.AddHealthChecks()
                .AddUrlGroup(new Uri("https://localhost:44362/swagger/index.html"),
                             name: "Swagger Feedback API",
                             failureStatus: HealthStatus.Unhealthy);
    }
}
