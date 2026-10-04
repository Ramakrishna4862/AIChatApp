using AIChatApp.Data;
using AIChatApp.Models;
using AIChatApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIChatApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly IAIService _aiServices;
        private readonly ApplicationDbContext _context;

        public ChatController(
            IAIService aiServices,
            ApplicationDbContext context)
        {
            _aiServices = aiServices;
            _context = context;
        }


        

        

        public async Task<IActionResult> Index(int? sessionId)
        {
            var sessions = await _context.ChatSessions
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

            if (sessionId == null)
            {
                if (sessions.Any())
                {
                    // Open the most recent existing chat
                    sessionId = sessions.First().Id;
                }
                else
                {
                    // Create a chat only when no chats exist
                    var session = new ChatSession
                    {
                        Title = "New Chat",
                        CreatedDate = DateTime.Now
                    };

                    _context.ChatSessions.Add(session);
                    await _context.SaveChangesAsync();

                    sessionId = session.Id;

                    sessions.Insert(0, session);
                }
            }

            var history = await _context.ChatMessages
                .Where(x => x.ChatSessionId == sessionId)
                .OrderBy(x => x.CreatedDate)
                .ToListAsync();

            ChatViewModel model = new ChatViewModel
            {
                ChatHistory = history,
                ChatSessionId = sessionId,
                ChatSessions = sessions
            };

            return View(model);
        }




        public async Task<IActionResult> History(
            string? searchText,
            int page = 1)
        {
            int pageSize = 10;

            var query = _context.ChatMessages.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(x =>
                    x.UserMessage.Contains(searchText) ||
                    x.AIResponse.Contains(searchText));
            }

            int totalRecords =
                await query.CountAsync();

            int totalPages =
                (int)Math.Ceiling(
                    (double)totalRecords / pageSize);

            var messages = await query
                .OrderByDescending(x => x.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.SearchText = searchText;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var chat =
                await _context.ChatMessages.FindAsync(id);

            if (chat != null)
            {
                _context.ChatMessages.Remove(chat);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("History");
        }

        public async Task<IActionResult> NewChat()
        {
            var session = new ChatSession
            {
                Title = "New Chat",
                CreatedDate = DateTime.Now
            };

            _context.ChatSessions.Add(session);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { sessionId = session.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ChatMessage chatMessage)
        {
            var history = await _context.ChatMessages
                           .Where(x => x.ChatSessionId == chatMessage.ChatSessionId)
                           .OrderBy(x => x.CreatedDate)
                           .ToListAsync();
            if (string.IsNullOrWhiteSpace(chatMessage.UserMessage))
            {
                return RedirectToAction("Index", new
                {
                    sessionId = chatMessage.ChatSessionId
                });
            }

            if (chatMessage.ChatSessionId == null)
            {
                return RedirectToAction("Index");
            }

            AIResponseResult aiResult =
                await _aiServices.GetResponseAsync(chatMessage.UserMessage, history);

            if (!aiResult.Success)
            {
                ChatViewModel errorModel = new ChatViewModel
                {
                    UserMessage = chatMessage.UserMessage,
                    AIResponse = aiResult.Error,
                    ChatSessionId = chatMessage.ChatSessionId
                };

                return View("Index", errorModel);
            }

            chatMessage.AIResponse = aiResult.Response;
            chatMessage.Provider = aiResult.Provider;
            chatMessage.CreatedDate = DateTime.Now;

            var session = await _context.ChatSessions.FindAsync(chatMessage.ChatSessionId);

            if(session !=null && session.Title == "New Chat")
            {
                session.Title = chatMessage.UserMessage;
            }

            _context.ChatMessages.Add(chatMessage);

            await _context.SaveChangesAsync();


            ChatViewModel model = new ChatViewModel
            {
                UserMessage = chatMessage.UserMessage,
                AIResponse = chatMessage.AIResponse,
                ChatHistory = history,
                ChatSessionId = chatMessage.ChatSessionId
            };

            return View("Index", model);
        }

        public async Task<IActionResult> DeleteSession(int id)
        {
            var session = await _context.ChatSessions.FindAsync(id);

            if(session == null)
            {
                return RedirectToAction("Index");
            }

            var messages = await _context.ChatMessages.Where(x => x.ChatSessionId == id).ToListAsync();

            _context.ChatMessages.RemoveRange(messages);
            _context.ChatSessions.Remove(session);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }

    }

