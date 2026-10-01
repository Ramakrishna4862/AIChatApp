using System.Collections.Generic;

namespace AIChatApp.Models
{
    public class ChatViewModel
    {
        public string? UserMessage { get; set; }
        public string? AIResponse { get; set; }
        public int? ChatSessionId { get; set; }
        public List<ChatMessage> ChatHistory { get; set; }
            = new List<ChatMessage>();
        public List<ChatSession> ChatSessions { get; set; } = new List<ChatSession>();
    }

}
