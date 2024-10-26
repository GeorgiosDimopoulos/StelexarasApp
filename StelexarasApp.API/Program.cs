using Microsoft.EntityFrameworkCore;
using StelexarasApp.API;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.DataAccess;
using StelexarasApp.Services.Mappers;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services for API layer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

// Configure dependencies
ConfigureServices(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger";
    });
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// Map endpoints
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.ConfigureHealthChecks(builder.Configuration);
    builder.Services.AddHealthChecks()
        .AddCheck<DbHealthCheck>("Database");

    // Register Repositories and Services used by API
    builder.Services.AddScoped<IPaidiRepository, PaidiRepository>();
    builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
    builder.Services.AddScoped<IDutyRepository, DutyRepository>();
    builder.Services.AddScoped<IStaffRepository, StaffRepository>();
    builder.Services.AddScoped<ITeamsRepository, TeamsRepository>();

    builder.Services.AddScoped<IStaffService, StaffService>();
    builder.Services.AddScoped<IPaidiaService, PaidiaService>();
    builder.Services.AddScoped<IExpenseService, ExpenseService>();
    builder.Services.AddScoped<ITeamsService, TeamsService>();
    builder.Services.AddScoped<IDutyService, DutyService>();

    // Add AutoMapper
    builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

    // Add DbContext
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Add Health Checks UI
    builder.Services.AddHealthChecksUI().AddInMemoryStorage();
}
