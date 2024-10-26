using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace StelexarasApp.API.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly IHubContext<MyHub> _hubContext;

    public NotificationsController(IHubContext<MyHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("broadcast")]
    public async Task<IActionResult> BroadcastMessage([FromBody] MessageModel messageModel)
    {
        // Broadcast the message to all connected clients via SignalR
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", messageModel.User, messageModel.Message);
        return Ok(new { Message = "Message broadcasted successfully" });
    }
}

public class MessageModel
{
    public string User { get; set; }
    public string Message { get; set; }
}

