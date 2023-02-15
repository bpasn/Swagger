using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swagger.Core.AuthService;
using Swagger.Core.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Swagger.Web.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authServices)
        {
            _authService = authServices;
        }

        // [HttpGet("GetUsers")]
        // public IActionResult GetUsers()
        // {
        //     return Ok(_authService.GetAuths());
        // }

        // [HttpGet("{id}", Name = "GetUser")]
        // public IActionResult GetUser(string id)
        // {
        //     return Ok(_authService.GetAuth(id));
        // }

        // [HttpPost("AddUser")]
        // public IActionResult AddUser(Auth auth)
        // {
        //     _authService.AddUser(auth);
        //     return CreatedAtRoute("GetUser", new { id = auth.Id }, auth);
        // }
        [HttpPost("insert-user")]
        public async Task<IActionResult> InsertUserAsync(AuthRegisterModel auth)
        {
            try
            {
                await _authService.InsertUser(auth);
                return Ok();
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}

