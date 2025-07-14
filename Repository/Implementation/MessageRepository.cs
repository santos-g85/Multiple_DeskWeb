using MongoDB.Driver;
using Multiple_Desk.Models.Entities;
using Multiple_Desk.Repository.Interface;
using Multiple_Desk.Settings;

namespace Multiple_Desk.Repository.Implementation
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ILogger<MessageRepository> _logger;
        private IMongoCollection<MessagesModel> _messageCollection;
        public MessageRepository(ILogger<MessageRepository> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            var collectionName = nameof(MessagesModel).Replace("Model", " ").ToLower();
            _messageCollection = context.GetCollection<MessagesModel>(collectionName);
        }
        public async Task<string> CreateMessageAsync(MessagesModel request)
        {
            try
            {
                await _messageCollection.InsertOneAsync(request);
                _logger.LogInformation("Message created successfully with EmailID: { email}", request.Email);
                return request.Email;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating message: {Message}", ex.Message);
                return null;
            }
        }
    }
}
