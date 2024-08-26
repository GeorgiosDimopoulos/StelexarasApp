using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Services;
using StelexarasApp.ViewModels;

var builder = WebApplication.CreateBuilder(args);
ConfigureServives(builder);

var app = builder.Build();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

        // c.RoutePrefix = "swagger";
        c.RoutePrefix = string.Empty;

    });
    app.UseDeveloperExceptionPage();
}

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/Koinotitas/Index");
        return;
    }

    await next();
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Koinotitas}/{action=Index}/{id?}");

app.Run();

void ConfigureServives(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
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

    builder.Services.AddControllersWithViews();
    builder.Services.AddControllers();
}