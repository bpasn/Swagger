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
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AuthenticateController(UserManager<ApplicationUser> userManager , RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpPost]
        [Route("roles/add")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseHandle))]
        public async Task<IActionResult> CreateRole([FromBody] RoleRequest request)
        {
            var addRole = new ApplicationRole { Name = request.Role };
            IdentityResult createRole = await _roleManager.CreateAsync(addRole);
            return createRole.Succeeded ? Ok(new { message = "Create roles success" }) : BadRequest(new ResponseHandle { Message = createRole?.Errors?.First()?.Description, Success = false });
        }


        [HttpPost]
        [Route("register")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseHandle))]

        public async Task<IActionResult> Register([FromBody] RegisRequest request)
        {
            var result = await RegisterAsync(request);
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

        private async Task<ResponseHandle> RegisterAsync(RegisRequest request)
        {
            try
            {
                var userExists = await _userManager.FindByEmailAsync(request.Email);
                if (userExists != null) return new ResponseHandle { Message = "User alreay exists", Success = false };


                //

                userExists = new ApplicationUser
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    UserName = request.Email
                };

                var createUserToResult = await _userManager.CreateAsync(userExists, request.Password);

                if (!createUserToResult.Succeeded) return new ResponseHandle { Message = $"Create User failed {createUserToResult?.Errors?.First()?.Description}", Success = false };

                var addUserToResult = await _userManager.AddToRoleAsync(userExists, "USER");
                if(!addUserToResult.Succeeded) return new ResponseHandle { Message = $"Create User Success but could not add user to role {addUserToResult?.Errors?.First()?.Description}", Success = false };

                return new ResponseHandle
                {
                    Message = "Register Success",
                    Success = true
                };
            }
            catch(Exception ex)
            {
                return new ResponseHandle
                {
                    Message = ex.Message,
                    Success = false
                };
            }
        }
        private async Task<ResponseHandle> LoginAsync(LoginRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

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
            catch (Exception ex)
            {
                return new ResponseHandle {
                    Message = ex.Message,
                    Success = false
                };
            }

        }
    }
}
