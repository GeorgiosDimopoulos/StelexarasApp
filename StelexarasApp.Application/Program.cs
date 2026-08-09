using MudBlazor.Services;
using Refit;
using StelexarasApp.Application.ApiClients;
using StelexarasApp.Application.Components;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseStaticWebAssets();

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddMudServices();

var apiBaseUrl = builder.Configuration["ApiBaseUrl"];
if (string.IsNullOrEmpty(apiBaseUrl))
{
    throw new InvalidOperationException("ApiBaseUrl configuration is missing.");
}

builder.Services.AddRefitClient<IKoinotitesApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseUrl));
builder.Services.AddRefitClient<ITomeisApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseUrl));
builder.Services.AddRefitClient<ISkinesApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseUrl));
builder.Services.AddRefitClient<IPaidiaApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseUrl));

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
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
