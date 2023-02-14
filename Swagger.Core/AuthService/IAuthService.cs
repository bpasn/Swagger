using System;
namespace Swagger.Core.AuthService
{
	public interface IAuthService
	{
		List<Auth> GetAuths();

		Auth GetAuth(string id);
		Auth AddUser(Auth auth);
	}
}

