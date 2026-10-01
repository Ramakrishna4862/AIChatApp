namespace AIChatApp.Models
{
    public class ChatSession
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public DateTime CreatedDate { get; set; }
        public List<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    }
}
