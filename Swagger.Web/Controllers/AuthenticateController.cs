using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swagger.Web.DTos;
using Swagger.Web.Models;

namespace Swagger.Web.Controllers
{
    [ApiController]
    [Route("api/v2/authenticate")]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthenticateController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        [Route("register")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseHandle))]

        public async Task<IActionResult> Register([FromBody] RegisRequest request)
        {
            var result = await RegisterAsnc(request);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPost]
        [Route("login")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseHandle))]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {

            var result = await LoginAsync(request);

            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        private async Task<ResponseHandle> RegisterAsnc(RegisRequest request)
        {

        }
        private async Task<ResponseHandle> LoginAsync(LoginRequest request)
        {
            try
            {

            }
            catch (Exception ex)
            {
                var user = await _userManager.FindByIdAsync(request.Email);

                if (User is null) return new ResponseHandle
                {
                    Message = "Invalid email/password",
                    Success = false
                };

                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
                var roles = await _userManager.GetRolesAsync(user);

                var roleClams = roles.Select(x => new Claim(ClaimTypes.Role, x));
                claims.AddRange(roleClams);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asgsdfgsdfgagsdfg"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var expires = DateTime.Now.AddMinutes(5);

                var token = new JwtSecurityToken(
                    issuer: "https:/localhost:5001",
                    audience: "https://localhost:5001",
                    claims: claims,
                    expires: expires,
                    signingCredentials: creds);


                return new ResponseHandle
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    Message = "Login Success",
                    Success = true,
                    Email = user.Email,
                    UserId = user?.Id.ToString()
                };
            }

        }
    }
}
