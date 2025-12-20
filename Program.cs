using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddMudServices();
var app = builder.Build();
app.MapRazorComponents<App>();
app.Run();