using Microsoft.EntityFrameworkCore;
using StelexarasApp.API;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.DataAccess;
using StelexarasApp.Services.Mappers;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Services.Services;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Services.Validators;
using StelexarasApp.API.Authorization;

var builder = WebApplication.CreateBuilder(args);

ConfigureJwtAuthenticationAndSwagger(builder);

// Add services for API layer
builder.Services.AddEndpointsApiExplorer();

// Configure dependencies
ConfigureServices(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "General V1");
        c.SwaggerEndpoint("/swagger/admin/swagger.json", "Admin API");
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

// Use Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    // Configure Health Checks
    builder.Services.ConfigureHealthChecks(builder.Configuration);
    builder.Services.AddHealthChecks().AddCheck<DbHealthCheck>("Database");
    builder.Services.AddHealthChecksUI().AddInMemoryStorage();

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

    builder.Services.AddScoped<IAuthTokenProvider, AuthTokenProvider>();

    // Add AutoMapper
    builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

    // Add DbContext
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Add FluentValidation Checks
    builder.Services.AddFluentValidationAutoValidation();

    // Register Validators
    builder.Services.AddTransient<IValidator<IStelexosDto>, StelexosValidator>();
    builder.Services.AddTransient<IValidator<PaidiDto>, PaidiValidator>();

    // Add Controllers
    builder.Services.AddControllers();
}

void ConfigureJwtAuthenticationAndSwagger(WebApplicationBuilder builder)
{
    var jwtSettings = builder.Configuration.GetSection("Jwt") ?? throw new Exception("Jwt section is missing in appsettings.json");
    var key = jwtSettings ["Key"] ?? throw new Exception("JWT Key is missing in appsettings.json");
    var keyBytes = Encoding.ASCII.GetBytes(key);

    builder.Services.AddSwaggerGen(options =>
    {
        options.EnableAnnotations();
        options.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });
        options.SwaggerDoc("admin", new() { Title = "Admin API", Version = "admin" });

        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorizatio",
            Type = SecuritySchemeType.ApiKey,
            In = ParameterLocation.Header,
            Scheme = "Bearer",
            Description = "JWT Authorization header using the Bearer scheme.",
        };
        options.AddSecurityDefinition("Bearer", securityScheme);

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { securityScheme, Array.Empty<string>() }
        });
    });

    // Configure Authentication
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidIssuer = jwtSettings ["Issuer"],
                ValidateIssuer = !string.IsNullOrEmpty(jwtSettings ["Issuer"]),
                ValidAudience = jwtSettings ["Audience"],
                ValidateAudience = !string.IsNullOrEmpty(jwtSettings ["Audience"]),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

    // Add Authorization
    builder.Services.AddAuthorization();
}