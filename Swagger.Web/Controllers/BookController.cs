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
        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult GetBook(string id)
        {
            return Ok(_bookservice.GetBook(id));
        }
        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            _bookservice.AddBook(book);
            return CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(string id)
        {
            _bookservice.DeleteBook(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(Book book)
        {
            try
            {
                return Ok(_bookservice.UpdateBook(book));
            }
            catch (System.Exception)
            {
               throw;
            }
        }
    }
}