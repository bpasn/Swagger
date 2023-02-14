using System;
using MongoDB.Driver;

namespace Swagger.Core.AuthService
{
	public interface IDbClientAuth
	{
		IMongoCollection<Auth> GetAuthCollection();
	}
}

