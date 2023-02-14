using Microsoft.AspNetCore.Mvc;
using Swagger.Core;

namespace Swagger.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private IBookServices _bookservice;

        public BookController(IBookServices bookservice)
        {
            _bookservice = bookservice;
        }
        [HttpGet]
        public IActionResult GetBook() 
        { 

            return Ok(_bookservice.GetBooks());
                
        }
    }
}