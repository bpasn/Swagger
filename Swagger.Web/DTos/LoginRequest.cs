using System.ComponentModel.DataAnnotations;

namespace Swagger.Web.DTos
{
    public class LoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
