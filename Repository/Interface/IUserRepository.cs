using Microsoft.AspNetCore.Mvc;
using Multiple_Desk.Models.DTOs;
namespace Multiple_Desk.Repository.Interface
{
    public interface IUserRepository
    {
        Task<UserRegisterDto?> CreateUserAsync(UserRegisterDto request);
    }
}
