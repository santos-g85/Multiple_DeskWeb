using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Multiple_Desk.Models.Entities;
using Multiple_Desk.Services.Interfaces;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Multiple_Desk.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IMessageService _messageService;
        private readonly IValidator<MessagesModel> _MessageValidator;
        public ContactController(ILogger<ContactController> logger,
            IMessageService messageService,
            IValidator<MessagesModel> validators)
        {
            _logger = logger;
            _messageService = messageService;
            _MessageValidator = validators;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SendMessage(MessagesModel request)
        {
       
            var validationResult = await _MessageValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Message validation failed: {Errors}", validationResult.Errors);
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                TempData["ValidationError"] = "Please correct the highlighted fields before submitting.";
                return View("Index", request);
            }

            await _messageService.SendMessage(request);
            _logger.LogInformation("message send successfully");
            TempData["Success"] = "Message Sent Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
