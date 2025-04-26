using StelexarasApp.API;

var builder = WebApplication.CreateBuilder(args);

// Add services for API layer
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureJwtAuthenticationAndSwagger(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Swagger UI configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseCors("AllowAll");
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"); // General
        //c.SwaggerEndpoint("/swagger/v2/swagger.json", "API v2"); // Admin
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
app.MapHealthChecks("/health");

app.Run();