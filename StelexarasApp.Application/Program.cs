using MudBlazor.Services;
using Refit;
using StelexarasApp.Application.ApiClients;
using StelexarasApp.Application.Components;
using StelexarasApp.Services.Mappers.Teams;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddMudServices();

builder.Services.AddRefitClient<IKoinotitesApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5010")); // or 7049
builder.Services.AddAutoMapper(cfg => { }, typeof(SkiniMappingProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
