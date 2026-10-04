using AIChatApp.Models;

namespace AIChatApp.Services
{
    public interface IAIService
    {
        Task<AIResponseResult> GetResponseAsync(string message, List<ChatMessage> history);
    }
}
