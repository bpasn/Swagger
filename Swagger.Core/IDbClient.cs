using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Swagger.Core.Models;

namespace Swagger.Core
{
    public interface IDbClient
    {
        IMongoCollection<Book> GetBooksCollection();

        IMongoCollection<AuthRegisterModel> GetUsersCollection();
    }
}
