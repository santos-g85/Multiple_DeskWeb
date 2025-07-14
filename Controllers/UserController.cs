using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Multiple_Desk.Models.DTOs;
using Multiple_Desk.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Multiple_Desk.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IValidator<UserRegisterDto> _userRegistorValidator;
        public UserController(ILogger<UserController> logger,
            IUserService userService,
            IValidator<UserRegisterDto> validator)
        {
            _logger = logger;
            _userService = userService;
            _userRegistorValidator = validator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Download/Index.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            var registorValidatorResult = await _userRegistorValidator.ValidateAsync(request);
            if (!registorValidatorResult.IsValid)
            {
                _logger.LogWarning($"user registration validation failed {registorValidatorResult.Errors}");
                foreach (var error in registorValidatorResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                TempData["ValidationError"] = "Please correct the highlighted fields before submitting.";
                return View("~/Views/Download/Index.cshtml", request);
            }
            var result = await _userService.RegisterUser(request);
            if (result is null)
            {
                _logger.LogWarning("User with email {Email} already exists", request.Email);
                TempData["UseCreated"] = " User registration successfull !";
                TempData["StartDownload"] = true;
                return RedirectToAction(nameof(Index));
            }
            TempData["UseCreated"] = " User registration successfull !";
            TempData["StartDownload"] = true;
            return RedirectToAction(nameof(Index));
        }
    }
}
