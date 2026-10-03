using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AIChatApp.Services
{
    public class OpenRouterAIService
    {
        private readonly HttpClient _httpClient;

        public OpenRouterAIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AIResponseResult> GetResponseAsync(string message)
        {
            string? apiKey =
                Environment.GetEnvironmentVariable("OPENROUTER_API_KEY");

            if (string.IsNullOrEmpty(apiKey))
            {
                return new AIResponseResult
                {
                    Success = false,
                    Error = "OpenRouter API key is not configured."
                };
            }

            string url =
                "https://openrouter.ai/api/v1/chat/completions";

            var requestBody = new
            {
                //model = "openai/gpt-oss-20b:free",
                model = "openrouter/free",
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = message
                    }
                }
            };

            string json =
                JsonSerializer.Serialize(requestBody);

            using var request =
                new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            request.Content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            try
            {
                using HttpResponseMessage response =
                    await _httpClient.SendAsync(request);

                string responseContent =
                    await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new AIResponseResult
                    {
                        Success = false,
                        Error =
                            "OpenRouter API Error: " +
                            responseContent
                    };
                }

                using JsonDocument document =
                    JsonDocument.Parse(responseContent);

                string? result =
                    document.RootElement
                        .GetProperty("choices")[0]
                        .GetProperty("message")
                        .GetProperty("content")
                        .GetString();

                if (string.IsNullOrEmpty(result))
                {
                    return new AIResponseResult
                    {
                        Success = false,
                        Error =
                            "No response received from OpenRouter."
                    };
                }

                return new AIResponseResult
                {
                    Success = true,
                    Response = result,
                    Provider = "OpenRouter"
                };
            }
            catch (TaskCanceledException)
            {
                return new AIResponseResult
                {
                    Success = false,
                    Error =
                        "OpenRouter API request timed out."
                };
            }
            catch (HttpRequestException)
            {
                return new AIResponseResult
                {
                    Success = false,
                    Error =
                        "Unable to connect to OpenRouter API."
                };
            }
            catch (Exception)
            {
                return new AIResponseResult
                {
                    Success = false,
                    Error =
                        "An unexpected error occurred while contacting OpenRouter."
                };
            }
        }
    }
}