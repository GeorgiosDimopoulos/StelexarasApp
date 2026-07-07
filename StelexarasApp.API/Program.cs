using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services for API layer
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureJwtAuthenticationAndSwagger(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
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
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

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