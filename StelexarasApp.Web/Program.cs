using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Mappers;
using StelexarasApp.Services.Services;
using StelexarasApp.ViewModels;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.ViewModels.TeamsViewModels;
using StelexarasApp.ViewModels.PeopleViewModels;
using StelexarasApp.Web;

var builder = WebApplication.CreateBuilder(args);
ConfigureServives(builder);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

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

// var redirectUrl = Environment.GetEnvironmentVariable("DEFAULT_REDIRECT_URL") ?? "/KoinotitaWeb/Index";
// var redirectUrl = ResourceHelper.GetValue("DefaultRedirectUrl");
var redirectUrl = builder.Configuration ["DefaultRedirectUrl"];

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        // context.Response.Redirect("/Koinotita/Index");
        context.Response.Redirect(redirectUrl);
        return;
    }

    await next();
});

// Middleware order
app.UseRouting();
app.UseStaticFiles();

app.MapHealthChecks("/health");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=KoinotitaWeb}/{action=Index}/{id?}");

app.MapControllers();
app.MapHub<MyHub>("/myhub");

app.Run();

void ConfigureServives(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<PaidiDto>();
    builder.Services.AddTransient<StelexosDto>();
    builder.Services.AddTransient<TomearxisDto>();
    builder.Services.AddTransient<OmadarxisDto>();
    builder.Services.AddTransient<KoinotarxisDto>();

    builder.Services.AddScoped<IPaidiRepository, PaidiRepository>();
    builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
    builder.Services.AddScoped<IDutyRepository, DutyRepository>();
    builder.Services.AddScoped<IStaffRepository, StaffRepository>();
    builder.Services.AddScoped<ITeamsRepository, TeamsRepository>();

    builder.Services.AddControllers();
    builder.Services.AddControllersWithViews();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddSignalR();

    builder.Services.AddHealthChecks()
        .AddCheck<DbHealthCheck>("Database");

    // builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    // Add DbContext with SQL Server
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Register your own application services
    builder.Services.AddScoped<IStaffService, StaffService>();
    builder.Services.AddScoped<IPaidiaService, PaidiaService>();
    builder.Services.AddScoped<IExpenseService, ExpenseService>();
    builder.Services.AddScoped<ITeamsService, TeamsService>();
    builder.Services.AddScoped<IDutyService, DutyService>();

    builder.Services.AddTransient(provider =>
    {
        var paidiaService = provider.GetRequiredService<IPaidiaService>();
        var teamsService = provider.GetRequiredService<ITeamsService>();
        return new SxoliViewModel(teamsService, paidiaService);
    });

    builder.Services.AddTransient(provider =>
    {
        var staffService = provider.GetRequiredService<IStaffService>();
        return new StaffViewModel(staffService, "Koinotita");
    });

    builder.Services.AddTransient(provider =>
    {
        var paidiaService = provider.GetRequiredService<IPaidiaService>();
        return new PaidiInfoViewModel(new PaidiDto(), paidiaService, "Skini");
    });

    builder.Services.AddTransient<ExpensesViewModel>();
    builder.Services.AddTransient<PaidiaViewModel>();
    builder.Services.AddTransient<StelexosInfoViewModel>();
    builder.Services.AddTransient<DutyViewModel>();
    builder.Services.AddTransient<SxoliViewModel>();
}