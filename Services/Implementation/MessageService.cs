using Multiple_Desk.Models.Entities;
using Multiple_Desk.Repository.Interface;
using Multiple_Desk.Services.Interfaces;

namespace Multiple_Desk.Services.Implementation
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ILogger<MessageService> _logger;
        public MessageService(ILogger<MessageService> logger,
            IMessageRepository messageRepository)
        {
            _logger = logger;
            _messageRepository = messageRepository;
        }
        public async Task<string> SendMessage(MessagesModel request)
        {
            if(request is null)
            {
                _logger.LogWarning("Received null request in SendMessage");
                return null;
            }
            await _messageRepository.CreateMessageAsync(request);
            _logger.LogInformation("Message sent successfully with EmailID: {Email}", request.Email);
            return request.Email;
        }
    }
}
