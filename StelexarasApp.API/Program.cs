using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureJwtAuthenticationAndSwagger(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddSignalR();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    const int maxAttempts = 6;
    for (var attempt = 1; ; attempt++)
    {
        try
        {
            db.Database.Migrate();
            break;
        }
        catch (Exception ex) when (attempt < maxAttempts && IsTransientSqlError(ex))
        {
            logger.LogWarning("Database not ready (attempt {Attempt}/{Max}): {Message}. Retrying in 15s...",
                attempt, maxAttempts, ex.Message);
            Thread.Sleep(TimeSpan.FromSeconds(15));
        }
    }
}

static bool IsTransientSqlError(Exception ex)
{
    int[] transient = [40613, 40197, 40501, 49918, 49919, 49920, 4060, 10928, 10929];
    for (var e = ex; e != null; e = e.InnerException!)
    {
        if (e is Microsoft.Data.SqlClient.SqlException sql && sql.Errors.Cast<Microsoft.Data.SqlClient.SqlError>().Any(x => transient.Contains(x.Number)))
            return true;
        if (e.InnerException == null) break;
    }
    return false;
}

var isDocker = app.Environment.IsEnvironment("Docker");

if (!isDocker)
{
    app.UseHttpsRedirection();
}

// Swagger UI configuration
if (app.Environment.IsDevelopment() || isDocker)
{
    var adminTitle = ApiConstants.ApiGroups.AdminTitle.ToLower();

    app.UseSwagger();
    app.UseCors("AllowAll");
    app.UseSwaggerUI(c =>
    {
        // c.SwaggerEndpoint($"/swagger/{adminTitle}/swagger.json", ApiConstants.ApiGroups.AdminInfo);
        // c.SwaggerEndpoint("/swagger/v2/swagger.json", ApiConstants.ApiGroups.PublicInfo);
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = "swagger";
    });
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Use Middleware
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapControllers();

app.MapHub<MyHub>("/myHub");

// Use Health Checks
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";
    options.ApiPath = "/health-api";
});

app.Run();