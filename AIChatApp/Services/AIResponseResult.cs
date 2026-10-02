namespace AIChatApp.Services
{
    public class AIResponseResult
    {
        public bool Success { get; set; }

        public string Response { get; set; } = string.Empty;

        public string Error { get; set; } = string.Empty;
    }
}