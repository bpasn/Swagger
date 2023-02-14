using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Swagger.Core.AuthService
{
	public class Auth

	{
		[BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
		public string? Id { get; set; }

		public string Name { get; set; }

		public string Pwd { get; set; }

		public string User_type { get; set; }

		public bool Status { get; set; }


	}
}

