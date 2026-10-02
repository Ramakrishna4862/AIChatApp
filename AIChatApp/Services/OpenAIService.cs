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
                ?? throw new InvalidOperationException(
                    "OpenAI API key is not configured.");

            _client = new ResponsesClient(apiKey);
        }

        public async Task<AIResponseResult> GetResponseAsync(string message)
        {
            try
            {
                ResponseResult response =
                    await _client.CreateResponseAsync("gpt-5.2", message);

                string result = response.GetOutputText();

                return new AIResponseResult
                {
                    Success = true,
                    Response = result
                };
            }
            catch (Exception ex)
            {
                return new AIResponseResult
                {
                    Success = false,
                    Error = "OpenAI API Error: " + ex.Message
                };
            }
        }
    }
}