using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Multiple_Desk.Models.DTOs;
using Multiple_Desk.Services.Implementation;
using Multiple_Desk.Services.Interfaces;

namespace Multiple_Desk.Controllers
{
    public class DownloadController : Controller
    {
        private readonly ILogger<DownloadController> _logger;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        private FileService _fileService;
        private readonly IValidator<UserRegisterDto> _userRegisterValidator;
        public DownloadController(ILogger<DownloadController> logger,
            IUserService userService,
             IWebHostEnvironment hostEnvironment,
            IConfiguration configuration,
            FileService fileService,
            IValidator<UserRegisterDto> validtor)
        {
            _logger = logger;
            _userService = userService;
            _webHostEnvironment = hostEnvironment;
            _config = configuration;
            _fileService = fileService;
            _userRegisterValidator = validtor;
        }
        
        public IActionResult Index()
        {  
          return View();
        }
    
        [HttpGet]
        public async Task<IActionResult> FileDownload()
        {
            var file = await _fileService.GetDownloadableFileAsync();
            if (file is null)
            {
                TempData["Error"] = "File could not be found.";
                return RedirectToAction(nameof(Index));
            }
            return file;
        }
        
    }
}
