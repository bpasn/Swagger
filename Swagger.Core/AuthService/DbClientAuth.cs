using System;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Swagger.Core.Models;

namespace Swagger.Core.AuthService
{
	public class DbClientAuth :IDbClientAuth
	{
        private readonly IMongoCollection<Auth> _auth;
        private readonly IMongoCollection<AuthRegisterModel> _Users;

        public DbClientAuth(IOptions<StoreDbConfig> authStoreDbConfig)
        {
            var client = new MongoClient(authStoreDbConfig.Value.Connection_String);
            var database = client.GetDatabase(authStoreDbConfig.Value.Database_Name);
            _auth = database.GetCollection<Auth>(authStoreDbConfig.Value.Auth_Collection_Name);
            
        }

        public IMongoCollection<Auth> GetAuthCollection() => _auth;

        public IMongoCollection<AuthRegisterModel> GetUserCollection() => _Users;
        
    }
}

