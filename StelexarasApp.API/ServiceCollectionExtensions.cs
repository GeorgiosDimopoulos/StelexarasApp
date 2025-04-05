using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.API.Authorization;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.DataAccess;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Services.Mappers;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Services.Services;
using StelexarasApp.Services.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace StelexarasApp.API;

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

        services.AddScoped<IStaffService, StaffService>();
        services.AddScoped<IPaidiaService, PaidiaService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<ITeamsService, TeamsService>();
        services.AddScoped<IDutyService, DutyService>();

        services.AddScoped<IAuthTokenProvider, AuthTokenProvider>();

        // Add AutoMapper
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        // Add DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Add FluentValidation Checks
        services.AddFluentValidationAutoValidation();

        // Register Validators
        services.AddTransient<IValidator<IStelexosDto>, StelexosValidator>();
        services.AddTransient<IValidator<PaidiDto>, PaidiValidator>();

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
            //string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //options.IncludeXmlComments(xmlPath);

            options.EnableAnnotations();
            options.SwaggerDoc("general", new() { Title = ApiConstants.ApiGroups.General, Version = "v1" });
            options.SwaggerDoc("admin", new() { Title = ApiConstants.ApiGroups.Admin, Version = "v1" });

            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out var methodInfo))
                    return false;

                var tags = methodInfo
                    .GetCustomAttributes(true)
                    .OfType<SwaggerOperationAttribute>()
                    .SelectMany(attr => attr.Tags)
                    .ToList();

                if (docName.Equals("general"))
                {
                    // return tags.Contains("General API");
                    return apiDesc.GroupName == "General API";
                }

                if (docName.Equals("admin"))
                {
                    // return tags.Contains("Admin API") || tags.Contains("General API");
                    return apiDesc.GroupName == "Admin API";
                }

                return false;
            });

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
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
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
        services.AddAuthorization();
    }
}
