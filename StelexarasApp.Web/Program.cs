using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Services;
using StelexarasApp.ViewModels;

var builder = WebApplication.CreateBuilder(args);
ConfigureServives(builder);

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

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/Koinotita/Index");
        return;
    }

    await next();
});

// Middleware order
app.UseRouting();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthorization();

// Map Controllers
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=koinotitaweb}/{action=Index}/{id?}");

app.Run();

void ConfigureServives(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddControllersWithViews();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add DbContext with SQL Server
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Register your own application services
    builder.Services.AddScoped<ITeamsService, TeamsService>();
    builder.Services.AddScoped<IExpenseService, ExpenseService>();
    builder.Services.AddScoped<IPersonalService, PersonalService>();
    builder.Services.AddScoped<IDutyService, DutyService>();

    // Register your ViewModels if they are used in the web layer (e.g., in controllers or Razor pages)
    builder.Services.AddTransient<TeamsViewModel>();
    builder.Services.AddTransient<ExpensesViewModel>();
    builder.Services.AddTransient<PersonalViewModel>();
    builder.Services.AddTransient<DutyViewModel>();
}