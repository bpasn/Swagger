using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Swagger.Core.Models;

namespace Swagger.Core
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<Book> _books;
        private readonly IMongoCollection<AuthRegisterModel> _users;
        public DbClient(IOptions<StoreDbConfig> StoreDbConfig)
        {
            var client = new MongoClient(StoreDbConfig.Value.Connection_String);
            var database = client.GetDatabase(StoreDbConfig.Value.Database_Name);
           
            _books = database.GetCollection<Book>(StoreDbConfig.Value.Books_Collection_Name);
           
            _users = database.GetCollection<AuthRegisterModel>(StoreDbConfig.Value.Users_Collection_Name);
        }
        public IMongoCollection<Book> GetBooksCollection() => _books;

        public IMongoCollection<AuthRegisterModel> GetUsersCollection() => _users;

    }
}
