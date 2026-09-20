#pragma warning disable OPENAI001

using OpenAI.Responses;



namespace AIChatApp.Services
{
    public class OpenAIService : IAIService
    {
        private readonly ResponsesClient _client;

        public OpenAIService(IConfiguration configuration)
        {
            string apiKey = configuration["OpenAI:ApiKey"]
                ?? throw new InvalidOperationException("OpenAI API key is not configured.");

            _client = new ResponsesClient(apiKey);
        }

        public async Task<string> GetResponseAsync(string message)
        {
            ResponseResult response =
                await _client.CreateResponseAsync("gpt-5.2", message);

            return response.GetOutputText();
        }
    }
}