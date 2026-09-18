namespace AIChatApp.Services
{
    public interface IAIService
    {
        Task<string> GetResponseAsync(string message);
    }
}
