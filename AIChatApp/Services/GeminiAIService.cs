using System.Text;
using System.Text.Json;
using AIChatApp.Models;

namespace AIChatApp.Services
{
    public class GeminiAIService : IAIService
    {
        private readonly HttpClient _httpClient;

        public GeminiAIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AIResponseResult> GetResponseAsync(string message, List<ChatMessage> history)
        {
            string conversationHistory = "";

            foreach (var chat in history)
            {
                conversationHistory +=
                    $"User: {chat.UserMessage}\n" +
                    $"AI: {chat.AIResponse}\n";
            }

            conversationHistory += $"User: {message}";

            string? apiKey =
                Environment.GetEnvironmentVariable("GEMINI_API_KEY");

            if (string.IsNullOrEmpty(apiKey))
            {
                return new AIResponseResult
                {
                    Success = false,
                    Error = "Gemini API key is not configured."
                };
            }

            string url = "https://generativelanguage.googleapis.com/v1beta/interactions";
            //string url = "https://generativelanguage.googleapis.com/v1beta/invalid";

            Console.WriteLine("========== HISTORY ==========");
            Console.WriteLine(conversationHistory);
            Console.WriteLine("=============================");

            var requestBody = new
            {
                model = "gemini-3.8-flash",
                input = conversationHistory
            };

            Console.WriteLine(JsonSerializer.Serialize(
    requestBody,
    new JsonSerializerOptions
    {
        WriteIndented = true
    }));

            string json = JsonSerializer.Serialize(requestBody);

            using var request =
                new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Add("x-goog-api-key", apiKey);

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
                    if ((int)response.StatusCode == 429)
                    {
                        return new AIResponseResult
                        {
                            Success = false,
                            Error = "Gemini request limit reached. Please try again later."
                        };
                    }

                    return new AIResponseResult
                    {
                        Success = false,
                        Error = "Gemini API Error: " + responseContent
                    };
                }

                using JsonDocument document =
                    JsonDocument.Parse(responseContent);

                JsonElement root = document.RootElement;

                foreach (JsonElement step in
                    root.GetProperty("steps").EnumerateArray())
                {
                    if (step.GetProperty("type").GetString() == "model_output")
                    {
                        foreach (JsonElement content in
                            step.GetProperty("content").EnumerateArray())
                        {
                            if (content.GetProperty("type").GetString() == "text")
                            {
                                string responseText =
                                    content.GetProperty("text").GetString()
                                    ?? "No response received.";

                                return new AIResponseResult
                                {
                                    Success = true,
                                    Response = responseText,
                                    Provider = "Gemini"
                                };
                            }
                        }
                    }
                }

                return new AIResponseResult
                {
                    Success = false,
                    Error = "No response received from Gemini."
                };
            }
            catch (TaskCanceledException)
            {
                return new AIResponseResult
                {
                    Success = false,
                    Error = "Gemini API request timed out. Please try again later."
                };
            }
            catch (HttpRequestException)
            {
                return new AIResponseResult
                {
                    Success = false,
                    Error = "Unable to connect to Gemini API. Please check your internet connection and try again."
                };
            }
            catch (Exception)
            {
                return new AIResponseResult
                {
                    Success = false,
                    Error = "An unexpected error occurred while contacting Gemini."
                };
            }
        }
    }
}