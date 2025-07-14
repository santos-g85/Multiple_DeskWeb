using Multiple_Desk.Models.DTOs;

namespace Multiple_Desk.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserRegisterDto?> RegisterUser(UserRegisterDto request);
    }
}
