using AspNetCore.Identity.MongoDbCore.Models;

namespace Swagger.Web.Models
{
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
    }
}
