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
            IAIService aiService,
            ApplicationDbContext context)
        {
            _aiServices = aiService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            string response =
                await _aiServices.GetResponseAsync(message);

            ChatViewModel model = new ChatViewModel
            {
                UserMessage = message,
                AIResponse = response
            };

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Models.ChatMessage chatMessage)
        {
            if (string.IsNullOrWhiteSpace(chatMessage.UserMessage))
            {
                ChatViewModel emptyModel = new ChatViewModel();

                return View("Index", emptyModel);
            }

            string aiResponse =
                await _aiServices.GetResponseAsync(chatMessage.UserMessage);

            if (aiResponse.StartsWith("Gemini API Error:") ||
                aiResponse == "Gemini API key is not configured." ||
                aiResponse == "No response received from Gemini.")
            {
                ChatViewModel errorModel = new ChatViewModel
                {
                    UserMessage = chatMessage.UserMessage,
                    AIResponse = aiResponse
                };

                return View("Index", errorModel);
            }

            chatMessage.AIResponse = aiResponse;
            chatMessage.CreatedDate = DateTime.Now;

            _context.ChatMessages.Add(chatMessage);

            await _context.SaveChangesAsync();

            ChatViewModel model = new ChatViewModel
            {
                UserMessage = chatMessage.UserMessage,
                AIResponse = chatMessage.AIResponse
            };

            return View("Index", model);
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
    }
}