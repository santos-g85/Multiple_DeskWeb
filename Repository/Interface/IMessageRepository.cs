using Multiple_Desk.Models.Entities;

namespace Multiple_Desk.Repository.Interface
{
    public interface IMessageRepository
    {
        Task<string> CreateMessageAsync(MessagesModel request);
    }
}
