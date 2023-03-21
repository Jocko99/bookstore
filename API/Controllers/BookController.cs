using Application;
using Application.Commands.Books;
using Application.DTO.Books;
using Application.Queries.Books;
using Application.Searches.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public BookController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<BookController>
        [HttpGet]
        public IActionResult Get([FromQuery] BookSearch search, [FromServices] IGetBookQuery query)
        {
            return Ok(_executor.ExecuteQuery(query,search));
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BookController>
        [HttpPost]
        public IActionResult Post([FromBody] BookCreateDto request, [FromServices] ICreateBookCommand command)
        {
            _executor.ExecuteCommand(command, request);
            return StatusCode(201);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BookDto request,[FromServices] IUpdateBookCommand command)
        {
            request.Id = id;
            _executor.ExecuteCommand(command, request);
            return NoContent();
        }

        [HttpPut("/api/book/authors/{id}")]
        public IActionResult ChangeAuthors(int id, [FromBody] BookAuthorDto request,[FromServices] IChangeAuthorCommand command)
        {
            request.BookId = id;
            _executor.ExecuteCommand(command, request);
            return NoContent();
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteBookCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
