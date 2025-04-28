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
            options.EnableAnnotations();

            //AddVersioning(options);

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
            
            //options.OperationFilter<AuthorizeCheckOperationFilter>();
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

        // Add Authorization
        services.AddAuthorization();
    }

    private static void AddVersioning(SwaggerGenOptions options)
    {
        options.SwaggerDoc(ApiConstants.VersionsGroups.v1, new() { Title = ApiConstants.VersionsGroups.v1, Version = ApiConstants.VersionsGroups.v1 });
        options.SwaggerDoc(ApiConstants.VersionsGroups.v2, new() { Title = ApiConstants.VersionsGroups.v2, Version = ApiConstants.VersionsGroups.v2 });

        options.DocInclusionPredicate((docName, apiDesc) =>
        {
            if (!apiDesc.TryGetMethodInfo(out var methodInfo))
                return false;

            var tags = methodInfo
                .GetCustomAttributes(true)
                .OfType<SwaggerOperationAttribute>()
                .SelectMany(attr => attr.Tags)
                .ToList();

            if (docName.Equals("v1"))
            {
                // return tags.Contains("General API");
                return apiDesc.GroupName == "v1";
            }

            if (docName.Equals("v2"))
            {
                // return tags.Contains("Admin API") || tags.Contains("General API");
                return apiDesc.GroupName == "v2 API";
            }

            return false;
        });

        //services.AddApiVersioning(options =>
        //{
        //    options.ReportApiVersions = true;
        //    options.AssumeDefaultVersionWhenUnspecified = true;
        //    options.DefaultApiVersion = new ApiVersion(1, 0);
        //    options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
        //                                            new HeaderApiVersionReader("x-api-version"),
        //                                            new MediaTypeApiVersionReader("x-api-version"));
        //});
        //services.AddVersionedApiExplorer(options =>
        //{
        //    options.GroupNameFormat = "'v'VVV";
        //    options.SubstituteApiVersionInUrl = true;
        //});
    }
}
