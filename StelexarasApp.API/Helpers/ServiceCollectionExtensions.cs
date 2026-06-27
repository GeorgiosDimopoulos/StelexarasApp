using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.DataAccess;
using StelexarasApp.Services.Mappers;
using StelexarasApp.Services.Services;
using StelexarasApp.Services.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.SwaggerGen;
using StelexarasApp.Services.Interfaces;
using StelexarasApp.Services.IServices;
using StelexarasApp.Library.Dtos;
using Microsoft.OpenApi.Models;

namespace StelexarasApp.API.Helpers;

public static class ServiceCollectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure Health Checks
        services.ConfigureHealthChecks(configuration);
        services.AddHealthChecks().AddCheck<DbHealthCheck>("Database");
        services.AddHealthChecksUI().AddInMemoryStorage();

        // Register Repositories and Services used by API
        services.AddScoped<IPaidiRepository, PaidiRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IDutyRepository, DutyRepository>();
        services.AddScoped<IStaffRepository, StaffRepository>();
        services.AddScoped<ITeamsRepository, TeamsRepository>();

        services.AddScoped<IStaffService<CreateStelexosRequest, UpdateStelexosRequest, DeleteStelexosRequest, StelexosResponse>, StaffService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<ITeamsService, TeamsService>();
        services.AddScoped<IDutyService, DutyService>();

        services.AddScoped<IAuthTokenProvider, AuthTokenProvider>();

        // Add AutoMapper
        services.AddAutoMapper(cfg => { }, typeof(ExpenseMappingProfile).Assembly);

        // Add DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Add FluentValidation Checks
        services.AddFluentValidationAutoValidation();

        // Register Validators
        services.AddTransient<IValidator<StelexosDtoBase>, StelexosValidator>();
        services.AddTransient<IValidator<PaidiDtoBase>, PaidiValidator>();
        services.AddTransient<IValidator<ExpenseDtoBase>, ExpenseValidator>();

        // Add Controllers
        services.AddControllers();
    }

    public static void ConfigureJwtAuthenticationAndSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("Jwt") ?? throw new Exception("Jwt section is missing in appsettings.json");
        var key = jwtSettings ["Key"] ?? throw new Exception("JWT Key is missing in appsettings.json");
        var keyBytes = Encoding.ASCII.GetBytes(key);

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API",
                Version = "v1"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter only the JWT token. The 'Bearer' prefix will be added automatically."
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

            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }
        });

        // Configure Authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Authentication failed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        Console.WriteLine("JWT CHALLENGE ERROR: " + context.ErrorDescription);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token validated!");
                        return Task.CompletedTask;
                    }
                };
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

        services.AddAuthorization();
    }

    private static void AddSwaggerAccessLevel(SwaggerGenOptions options)
    {
        options.SwaggerDoc(ApiConstants.ApiGroups.AdminTitle,
                           new()
                           {
                               Title = ApiConstants.ApiGroups.AdminInfo,
                               Version = ApiConstants.VersionsGroups.v1
                           });
        options.SwaggerDoc(ApiConstants.ApiGroups.PublicTitle,
                           new()
                           {
                               Title = ApiConstants.ApiGroups.PublicInfo,
                               Version = ApiConstants.VersionsGroups.v1
                           });

        options.DocInclusionPredicate((docName, apiDesc) =>
        {
            if (!apiDesc.TryGetMethodInfo(out var methodInfo))
                return false;

            var groupName = apiDesc.GroupName;

            return docName == groupName;
        });
    }
}
