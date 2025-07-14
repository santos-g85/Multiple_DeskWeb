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
        private FileService _fileService;
        public DownloadController(ILogger<DownloadController> logger,
            FileService fileService,
            IValidator<UserRegisterDto> validtor)
        {
            _logger = logger;
            _fileService = fileService;
        }
        
        public IActionResult Index()
        {  
          return View();
        }
    
        [HttpGet]
        public async Task<IActionResult> FileDownload()
        {
            var file = await _fileService.GetDownloadableFileAsync();
            _logger.LogInformation("Downloaded file !");
            if (file is null)
            {
                TempData["Error"] = "File could not be found.";
                _logger.LogWarning("file not found!");
                return RedirectToAction(nameof(Index));
            }
            _logger.LogInformation("File returned to the user!");
            return file;
        }
        
    }
}
