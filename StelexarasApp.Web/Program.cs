using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.Mappers;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Services.Services;
using StelexarasApp.Web.Validators;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

var app = builder.Build();

// Middleware and routing configuration
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Map default MVC routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    // Add DbContext
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

    builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

    // Add MVC services
    builder.Services.AddControllersWithViews();

    // Add FluentValidation
    builder.Services.AddValidatorsFromAssemblyContaining<StelexosValidator>();
    builder.Services.AddValidatorsFromAssemblyContaining<XwrosValidator>();
    builder.Services.AddValidatorsFromAssemblyContaining<PaidiValidator>();
    builder.Services.AddValidatorsFromAssemblyContaining<ExpenseValidator>();
    builder.Services.AddValidatorsFromAssemblyContaining<DutyValidator>();
}
