using System;
using MongoDB.Driver;
using Swagger.Core.Models;

namespace Swagger.Core.AuthService
{
	public interface IDbClientAuth
	{
		IMongoCollection<Auth> GetAuthCollection();
		IMongoCollection<AuthRegisterModel> GetUserCollection();
	}
}

