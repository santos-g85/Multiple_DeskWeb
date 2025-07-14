using Microsoft.AspNetCore.Identity;

namespace Multiple_Desk.Models.Entities
{
    public class UserModel :   IdentityUser
    {
        public string FirstName { get; set; } =string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? CompanyName { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;

    }
}
