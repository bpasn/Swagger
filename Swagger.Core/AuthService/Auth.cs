using MongoDB.Bson.Serialization.Attributes;
namespace Swagger.Core.AuthService
{
    public class Auth

    {
        private string pwd;

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        public string Pwd
        {
            get
            {
                return this.pwd;
            }
            set
            {
                this.pwd = Helper.HashCode(value); ;
            }
        }

        public string User_type { get; set; }

        public bool Status { get; set; }



    }
}

