using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Multiple_Desk.Models;

namespace Multiple_Desk.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Term()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangeLanguage(string language)
        {
            HttpContext.Session.SetString("Culture", language);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
