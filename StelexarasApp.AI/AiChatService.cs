using Microsoft.Extensions.AI;
using OllamaSharp;

namespace StelexarasApp.AI;

public class AiChatService
{
    private readonly IChatClient _chatClient;

    public AiChatService()
    {
        _chatClient = new OllamaApiClient(new Uri("http://localhost:11434"), "llama3.2");
    }

    public async Task<string> AskAsync(string question)
    {
        var response = await _chatClient.GetResponseAsync(question);
        return response.Text;
    }
}
