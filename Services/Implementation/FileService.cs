using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Multiple_Desk.Services.Implementation
{

    public class FileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<FileService> _logger;
        private readonly IConfiguration _config;
        public FileService(ILogger<FileService> logger,
            IWebHostEnvironment hostEnvironment,
            IConfiguration configuration
            )
        {
            _logger = logger;
            _webHostEnvironment = hostEnvironment;
            _config = configuration;
        }
        [HttpGet]
        public async Task<FileResult?> GetDownloadableFileAsync()
        {
            string downloadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Download");
            string fileName = _config.GetValue<string>("filename")!;
            string filePath = Path.Combine(downloadFolder, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                _logger.LogWarning("File not found: {filePath}", filePath);
                return null;
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            byte[] fileBytes;
            try
            {
                fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading file: {filePath}", filePath);
                return null;
            }

            _logger.LogInformation("Returning file: {fileName}", fileName);
            return new FileContentResult(fileBytes, contentType)
            {
                FileDownloadName = fileName
            };
        }
    }
}
