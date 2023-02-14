using System;
using MongoDB.Driver;

namespace Swagger.Core.AuthService
{
	public class AuthServices: IAuthService
	{
        private readonly IMongoCollection<Auth> _auth;
		public AuthServices(IDbClientAuth authCli)
		{
            _auth = authCli.GetAuthCollection();
		}

        public Auth AddUser(Auth auth)
        {
            _auth.InsertOne(auth);
            return auth;
        }

        //Get all user;
        List<Auth> IAuthService.GetAuths() => _auth.Find(auth => true).ToList();
        Auth IAuthService.GetAuth(string id) => _auth.Find<Auth>(auth => auth.Id == id).First();
        
    }
}

