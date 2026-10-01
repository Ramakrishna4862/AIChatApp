namespace AIChatApp.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }

        public string UserMessage { get; set; } = string.Empty;

        public string AIResponse { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? ChatSessionId { get; set; }
        public ChatSession? ChatSession { get; set; }   
    }
}