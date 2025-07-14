using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Multiple_Desk.Settings
{
    public class ApplicationDbContext
    {
        private readonly IMongoDatabase _database;

        public ApplicationDbContext(IOptions<MongoDbSettings> settings)
        {
            var mongoDbSettings = settings.Value;
            if(string.IsNullOrEmpty(mongoDbSettings.ConnectionString)
                || string.IsNullOrEmpty(mongoDbSettings.DatabaseName))
            {
                throw new ArgumentException("MongoDB settings are not properly configured.");
            }
            var client = new MongoClient(mongoDbSettings.ConnectionString);
            _database = client.GetDatabase(mongoDbSettings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}
