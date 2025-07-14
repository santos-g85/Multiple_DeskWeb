using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Multiple_Desk.Models.DTOs;
using Multiple_Desk.Models.Entities;
using Multiple_Desk.Repository.Interface;
using Multiple_Desk.Settings;
using System.Threading.Tasks;

namespace Multiple_Desk.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly IMongoCollection<UserModel> _userCollection;

        public UserRepository(ILogger<UserRepository> logger, ApplicationDbContext context)
        {
            _logger = logger;
            var collectionName = nameof(UserModel).Replace("Model", "s").ToLower();
            _userCollection = context.GetCollection<UserModel>(collectionName);
        }

        public async Task<UserRegisterDto?> CreateUserAsync(UserRegisterDto request)
        {
            // Check if user already exists
            var existingUser = await _userCollection.Find(x => x.Email == request.Email).FirstOrDefaultAsync();
            if (existingUser is  not null)
            {
                _logger.LogWarning("User with email {Email} already exists", request.Email);
                return default; 
            }
            try
            {
                var newUser = new UserModel
                {
                    Email = request.Email,
                    UserName=request.Email.Split('@')[0],
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    CompanyName = request.Company,
                    Country = request.Country,
                };
                await _userCollection.InsertOneAsync(newUser);
                return new UserRegisterDto
                {
                    Email = newUser.Email,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    PhoneNumber = newUser.PhoneNumber,
                    Company = newUser.CompanyName,
                    Country = newUser.Country
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error checking for existing user with email {Email}", request.Email);
                return null;
            }
            
        }
    }
}
