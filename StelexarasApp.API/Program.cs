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

var builder = WebApplication.CreateBuilder(args);

// Add services for API layer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        In = ParameterLocation.Header
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
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

    // Add JWt Authentication
    var jwtSettings = builder.Configuration.GetSection("Jwt") ?? throw new Exception("Jwt section is missing in appsettings.json");
    var key = Encoding.ASCII.GetBytes(jwtSettings ["Key"]!);
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // or "JwtBearer"
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = jwtSettings ["Issuer"] == null ? false : true,
            ValidateAudience = jwtSettings ["Audience"] == null ? false : true,
            ValidateLifetime = true
        };
    });

    builder.Services.AddAuthorization();

    // Add Controllers
    builder.Services.AddControllers();
}
