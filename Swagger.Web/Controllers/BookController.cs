using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swagger.Core;
using Swagger.Web.DTos;

namespace Swagger.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/books")]
    public class BookController : ControllerBase
	{
        private IBookServices _bookservice;
        public BookController(IBookServices bookServices)
        {
            _bookservice = bookServices;
        }
        
        [HttpGet]
        [Route("get")]
        public IActionResult GetBooks()
        {
            return Ok(_bookservice.GetBooks());
        }

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult GetBooks(string id)
        {
            return Ok(_bookservice.GetBook(id));
        }

        [HttpPost]
        [Route("post")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ResponseHandle))]
        public IActionResult AddBook(Book book)
        {
             _bookservice.AddBook(book);
            return CreatedAtRoute("GetUser", new { id = book.Id }, book);

        }
    }
}

