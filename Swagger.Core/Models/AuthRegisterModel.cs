using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Swagger.Core.Models
{
    public class AuthModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string First_name { get; set; }

        public string Last_name { get; set; }

        public string? Address { get; set; }

        public DateTime Birth_day { get; set; }

        public bool Status { get; set; } = true;
    }
}