using Application;
using Application.Commands.Genres;
using Application.DTO.Genres;
using Application.Queries.Genre;
using Application.Searches.Genre;
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
    public class GenreController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public GenreController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<GenreController>
        [HttpGet]
        public IActionResult Get([FromQuery] GenreSearch search,[FromServices] IGetGenreQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
                
        }

        // POST api/<GenreController>
        [HttpPost]
        public IActionResult Post([FromBody] GenreDto request, [FromServices] ICreateGenreCommand command)
        {
            _executor.ExecuteCommand(command, request);
            return StatusCode(201);
        }

        // PUT api/<GenreController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GenreDto request, [FromServices] IUpdateGenreCommand command)
        {
            request.Id = id;
            _executor.ExecuteCommand(command, request);
            return NoContent();
        }

        // DELETE api/<GenreController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,[FromServices] IDeleteGenreCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
