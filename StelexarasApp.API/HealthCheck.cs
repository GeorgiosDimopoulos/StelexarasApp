using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace StelexarasApp.API;

public static class HealthCheck
{
    public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddSqlServer(configuration ["ConnectionStrings:DefaultConnection"], healthQuery: "select 1", name: "SQL Server", failureStatus: HealthStatus.Unhealthy, tags: ["Feedback", "Database"]);
        services.AddHealthChecksUI(opt =>
        {
            opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
            opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
            opt.SetApiMaxActiveRequests(1); //api requests concurrency    
            opt.AddHealthCheckEndpoint("feedback api", "/api/health"); //map health check api    

        }).AddInMemoryStorage();
    }
}
