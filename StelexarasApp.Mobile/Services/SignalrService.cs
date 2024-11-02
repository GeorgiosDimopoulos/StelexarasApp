
using Microsoft.AspNetCore.SignalR.Client;

namespace StelexarasApp.Services;

public class SignalrService
{
    private readonly HubConnection _hubConnection;

    public SignalrService()
    {
        _hubConnection = new HubConnectionBuilder().WithUrl("https://yourserver.com/signalrhub").Build();
    }

    public async Task StartConnectionAsync()
    {
        await _hubConnection.StartAsync();
    }

    public async Task SendMessageAsync(string message)
    {
        await _hubConnection.InvokeAsync("SendMessage", message);
    }

    public void OnMessageReceived(Action<string> handler)
    {
        _hubConnection.On("ReceiveMessage", handler);
    }
}
