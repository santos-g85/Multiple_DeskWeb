using Multiple_Desk.Models.DTOs;
using Multiple_Desk.Repository.Interface;
using Multiple_Desk.Services.Interfaces;

namespace Multiple_Desk.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        public UserService(ILogger<UserService> logger,
            IUserRepository userRepository)
        {
           _logger = logger;
           _userRepository = userRepository;
        }
       
        public async Task<UserRegisterDto?> RegisterUser(UserRegisterDto request)
        {
           var result  = await _userRepository.CreateUserAsync(request);
            if(result is null)
            {
                _logger.LogWarning("User with email {Email} already exists", request.Email);
                return null; // User already exists
            }
            _logger.LogInformation("User with email {Email} created successfully", request.Email);
            return result; // User created successfully
        }
    }
}
