using System.ComponentModel.DataAnnotations;

namespace Swagger.Web.DTos
{
    public class RegisRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
