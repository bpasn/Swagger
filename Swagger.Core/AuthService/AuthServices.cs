using System;
using MongoDB.Driver;
using Swagger.Core.Models;

namespace Swagger.Core.AuthService
{
    public class AuthServices : IAuthService
    {
        private readonly IMongoCollection<Auth> _auth;
        private readonly IMongoCollection<AuthRegisterModel> _user;
        public AuthServices(IDbClient authCli)
        {
            // _auth = authCli.GetAuthCollection();
            _user = authCli.GetUsersCollection();
        }

        public async Task<Auth> AddUser(Auth auth)
        {
            await _auth.InsertOneAsync(auth);
            return auth;
        }

        //Get all user;
        List<Auth> IAuthService.GetAuths() => _auth.Find(auth => true).ToList();
        Auth IAuthService.GetAuth(string id) => _auth.Find<Auth>(auth => auth.Id == id).First();

        public async Task InsertUser(AuthRegisterModel auth)
        {
            try
            {
                await _user.InsertOneAsync(auth);
            }
            catch (MongoException error)
            {
                throw error.GetBaseException();
            }
        }

    }
}

