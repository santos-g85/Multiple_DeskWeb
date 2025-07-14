using Multiple_Desk.Models.Entities;

namespace Multiple_Desk.Services.Interfaces
{
    public interface IMessageService
    {
        Task<string> SendMessage(MessagesModel request);
    }
}
