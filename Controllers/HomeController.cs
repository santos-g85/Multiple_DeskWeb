using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Multiple_Desk.Models;
using Multiple_Desk.Models.Entities;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

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
            var events = GetEvents();
            ViewData["Events"] = events;
            return View();
        }

        public IActionResult Privacy()
        {
            return View("~/Views/Home/Privacy");
        }
        public IActionResult Term()
        {
            return View();
        }

  
        [HttpPost]
        public IActionResult ChangeLanguage(string language, string returnUrl)
        {
            Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(language)),
                 new CookieOptions
                 {
                 Expires = DateTimeOffset.UtcNow.AddYears(1),
                IsEssential = true, 
                Path = "/"
                 }
                );
            return LocalRedirect(returnUrl ?? "/");
        }

        private List<EventsModel>? GetEvents() 
        { 
            var events = new List<EventsModel>
            {
                new EventsModel
                {
                    ImagePath =  "~/images/freestocks-I_pOqP6kCOI-unsplash.jpg",
                    Title = "Vulnerability Assessment and Penetration Testing",
                    
                },
                new EventsModel
                {
                    ImagePath = "~/images/freestocks-I_pOqP6kCOI-unsplash.jpg",
                    Title = "Beta versions gone through alpha testing in-house and externally",
                  
                },
                new EventsModel
                {
                    ImagePath = "~/images/freestocks-I_pOqP6kCOI-unsplash.jpg",
                    Title = "Beta versions released to the public",
                   
                },
                 new EventsModel
                {
                    ImagePath = "~/images/freestocks-I_pOqP6kCOI-unsplash.jpg",
                    Title = "Beta versions user feedback collection",
                   
                }
            };
            return events;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
