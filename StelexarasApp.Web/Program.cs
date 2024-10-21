using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.Services;
using StelexarasApp.ViewModels;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.ViewModels.TeamsViewModels;
using StelexarasApp.ViewModels.PeopleViewModels;
using StelexarasApp.Web;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.Mappers;

var builder = WebApplication.CreateBuilder(args);

ConfigureServives(builder);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();
await SeedDbWithMockData(app); // SeedDbWithMockData(app).Wait();

var koinotitaRedirectUrl = builder.Configuration ["KoinotitaRedirectUrl"];
var dutiesRedirectUrl = builder.Configuration ["DutiesRedirectUrl"];
var expensesRedirectUrl = builder.Configuration ["ExpensesRedirectUrl"];

// Middleware order
app.UseRouting();

// Map routes for controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=KoinotitaWeb}/{action=Index}/{id?}",
    defaults: new { controller = "KoinotitaWeb", action = "Index" });
app.MapControllers();
app.MapHub<MyHub>("/myhub");

app.Use(async (context, next) =>
{
    var path = context.Request.Path.ToString();

    if (string.IsNullOrEmpty(path))
    {
        Console.WriteLine("Redirecting to default: /StaffWeb/Index");
        context.Response.Redirect("/StaffWeb/Index");
        return;
    }
    else if (context.Request.Path == "/")
    {
        Console.WriteLine("Redirecting to /: /KoinotitaWeb/Index");
        context.Response.Redirect("KoinotitaWeb/Index");
        return;
    }
    else if (context.Request.Path == "")
    {
        Console.WriteLine("Redirecting to /: /KoinotitaWeb/Index");
        context.Response.Redirect("KoinotitaWeb/Index");
        return;
    }
    else if (path.StartsWith("/koinotita", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine($"Redirecting to Koinotita RedirectUrl: {koinotitaRedirectUrl}");
        context.Response.Redirect(koinotitaRedirectUrl!);
        return;
    }
    else if (path.StartsWith("/duties", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine($"Redirecting to duties RedirectUrl: {dutiesRedirectUrl}");
        context.Response.Redirect(dutiesRedirectUrl!);
        return;
    }
    else if (path.StartsWith("/expenses", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine($"Redirecting to ExpensesRedirectUrl: {expensesRedirectUrl}");
        context.Response.Redirect(expensesRedirectUrl!);
        return;
    }

    Console.WriteLine("No redirect, processing request further.");
    await next();
});

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapHealthChecks("/health");

// Swagger setup
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "swagger/{documentName}/swagger.json";
    });

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger";
    });

    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.Run();

void ConfigureServives(WebApplicationBuilder builder)
{
    // builder.Services.AddScoped<DtoDataSeeder>();
    builder.Services.AddScoped<DataSeeder>();

    // register Repositories
    builder.Services.AddScoped<IPaidiRepository, PaidiRepository>();
    builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
    builder.Services.AddScoped<IDutyRepository, DutyRepository>();
    builder.Services.AddScoped<IStaffRepository, StaffRepository>();
    builder.Services.AddScoped<ITeamsRepository, TeamsRepository>();

    // register Services
    builder.Services.AddScoped<IStaffService, StaffService>();
    builder.Services.AddScoped<IPaidiaService, PaidiaService>();
    builder.Services.AddScoped<IExpenseService, ExpenseService>();
    builder.Services.AddScoped<ITeamsService, TeamsService>();
    builder.Services.AddScoped<IDutyService, DutyService>();

    // register simpel ViewModels
    builder.Services.AddTransient<ExpensesViewModel>();
    builder.Services.AddTransient<PaidiaViewModel>();
    builder.Services.AddTransient<DutyViewModel>();
    builder.Services.AddTransient<SxoliViewModel>();
    builder.Services.AddTransient<StaffViewModel>();

    // register ViewModels Constructor Dependencies
    builder.Services.AddScoped<PaidiInfoViewModel>(provider =>
        new PaidiInfoViewModel(new PaidiDto(), provider.GetRequiredService<IPaidiaService>(), "Skini"));
    builder.Services.AddScoped<StelexosInfoViewModel>(provider =>
        new StelexosInfoViewModel(null!, provider.GetRequiredService<IStaffService>()));

    // MVC and Health Checks
    builder.Services.AddControllers();
    builder.Services.AddControllersWithViews();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.EnableAnnotations();
    });
    builder.Services.AddSignalR();
    builder.Services.AddHealthChecks()
        .AddCheck<DbHealthCheck>("Database");

    // Add AutoMapper
    builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
    builder.Services.AddLogging();

    // Add DbContext with SQL Server
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

static async Task SeedDbWithMockData(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var dataSeeder = services.GetRequiredService<DataSeeder>(); // DtoDataSeeder
        await dataSeeder.SeedDutiesData();
        await dataSeeder.SeedExpensesData();
        await dataSeeder.SeedTeamsAndStaffData();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
    }
}