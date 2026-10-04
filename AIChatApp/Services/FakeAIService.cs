using AIChatApp.Models;

namespace AIChatApp.Services
{
    public class FakeAIService : IAIService
    {
        public async Task<AIResponseResult> GetResponseAsync(string message, List<ChatMessage> history)
        {
            await Task.Delay(1000);

            string response;

            if (message.ToLower().Contains("what is c#"))
            {
                response = "C# is a programming language developed by Microsoft.";
            }
            else if (message.ToLower().Contains("what is sql"))
            {
                response = "SQL is used to store, retrieve, and manage data in databases.";
            }
            else if (message.ToLower().Contains("what is asp.net core"))
            {
                response = "ASP.NET Core is a cross-platform framework used to build web applications and APIs.";
            }
            else
            {
                response = "This is a temporary local AI response.";
            }

            return new AIResponseResult
            {
                Success = true,
                Response = response
            };
        }
    }
}