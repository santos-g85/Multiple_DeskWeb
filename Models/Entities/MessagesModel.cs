namespace Multiple_Desk.Models.Entities
{
    public class MessagesModel
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; }= string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? Company { get; set; } 

        public string PhoneNumber { get; set; } = string.Empty;

        public string Country { get; set; } = "Nepal";

        public string Message { get; set; } = string.Empty;
    }
}
