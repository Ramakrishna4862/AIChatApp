namespace AIChatApp.Services
{
    public class FakeAIService : IAIService
    {
        public async Task<string> GetResponseAsync(string message)
        {
            await Task.Delay(1000);

            return "AI says: " + message;
        }
    }
}