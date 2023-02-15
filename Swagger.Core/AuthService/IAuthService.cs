using System;
using Swagger.Core.Models;

namespace Swagger.Core.AuthService
{
	public interface IAuthService
	{
		List<Auth> GetAuths();
		Auth GetAuth(string id);
		Task<Auth> AddUser(Auth auth);

		Task InsertUser(AuthRegisterModel auth);
	}
}

