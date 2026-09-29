namespace AIChatApp.Services
{
    public class FakeAIService : IAIService
    {
        public async Task<string> GetResponseAsync(string message)
        {
            await Task.Delay(1000);

            string userMessage = message.ToLower().Trim();

            if (userMessage.Contains("what is c#"))
            {
                return "C# is a programming language developed by Microsoft. It is commonly used to build web, desktop, mobile, and backend applications.";
            }

            if (userMessage.Contains("what is sql"))
            {
                return "SQL is a language used to communicate with relational databases. It is commonly used to insert, update, delete, and retrieve data.";
            }

            if (userMessage.Contains("what is asp.net core"))
            {
                return "ASP.NET Core is a cross-platform framework from Microsoft used to build web applications, Web APIs, and backend services.";
            }

            return "I am a temporary local AI service. I don't have a real AI model connected right now, but I received your message: " + message;
        }
    }
}