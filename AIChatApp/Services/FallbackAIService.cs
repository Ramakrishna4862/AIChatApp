using AIChatApp.Models;

namespace AIChatApp.Services
{
    public class FallbackAIService : IAIService
    {
        private readonly GeminiAIService _geminiAIService;
        private readonly GroqAIService _groqAIService;
        private readonly OpenRouterAIService _openRouterAIService;

        public FallbackAIService(
            GeminiAIService geminiAIService,
            GroqAIService groqAIService,
            OpenRouterAIService openRouterAIService)
        {
            _geminiAIService = geminiAIService;
            _groqAIService = groqAIService;
            _openRouterAIService = openRouterAIService;
        }

        public async Task<AIResponseResult> GetResponseAsync(string message, List<ChatMessage> history)
        {
            // 1. Try Gemini
            AIResponseResult geminiResult =
                await _geminiAIService.GetResponseAsync(message, history);

            if (geminiResult.Success)
            {
                return geminiResult;
            }

            // 2. Gemini failed → Try Groq
            AIResponseResult groqResult =
                await _groqAIService.GetResponseAsync(message, history);

            if (groqResult.Success)
            {
                return groqResult;
            }

            // 3. Gemini + Groq failed → Try OpenRouter
            AIResponseResult openRouterResult =
                await _openRouterAIService.GetResponseAsync(message, history);

            if (openRouterResult.Success)
            {
                return openRouterResult;
            }

            // All providers failed
            return new AIResponseResult
            {
                Success = false,
                Error =
                    "All AI providers failed. " +
                    "Gemini: " + geminiResult.Error +
                    " | Groq: " + groqResult.Error +
                    " | OpenRouter: " + openRouterResult.Error
            };
        }
    }
}