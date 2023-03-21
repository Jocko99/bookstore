using Application;
using Application.Commands.Authors;
using Application.DTO.Authors;
using Application.Queries.Authors;
using Application.Searches.Authors;
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
    public class AuthorController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public AuthorController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<AuthorController>
        [HttpGet]
        public IActionResult Get([FromQuery] AuthorSearch search, [FromServices] IGetAuthorQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }


        // POST api/<AuthorController>
        [HttpPost]
        public IActionResult Post([FromBody] AuthorDto request, [FromServices] ICreateAuthorCommand command)
        {
            _executor.ExecuteCommand(command, request);
            return StatusCode(201);
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AuthorDto request, [FromServices] IUpdateAuthorCommand command)
        {
            request.Id = id;
            _executor.ExecuteCommand(command, request);
            return NoContent();
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,[FromServices] IDeleteAuthorCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
