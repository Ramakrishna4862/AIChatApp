using AIChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AIChatApp.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<ChatMessage> ChatMessages { get; set; }    
        public DbSet<ChatSession> ChatSessions { get; set; }
    }
}
