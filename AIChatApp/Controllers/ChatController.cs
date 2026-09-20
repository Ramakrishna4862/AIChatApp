using AIChatApp.Services;
using Microsoft.AspNetCore.Mvc;
using AIChatApp.Models;

namespace AIChatApp.Controllers
{
    public class ChatController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult SendMessage(string message)
        //{
        //    return Content("You entered: " + message);
        //}

        private readonly IAIService _aiServices;
        public ChatController(IAIService aiService)
        {
            _aiServices = aiService;
        }
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> SendMessage(string message)
        //{
        //    string response = await _aiServices.GetResponseAsync(message);
        //    return Content(response);
        //}

        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            string response = await _aiServices.GetResponseAsync(message);
            ChatViewModel model = new ChatViewModel
            {
                UserMessage = message,
                AIResponse = response
            };
            return View("index", model);
        }
    }
}
