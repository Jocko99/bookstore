using Application;
using Application.Commands.Users;
using Application.DTO.Users;
using Application.Queries.Users;
using Application.Searches.Users;
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
    public class UserController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public UserController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get([FromQuery] UserSearch search,[FromServices] IGetUserQuery query)
        {
            return Ok(_executor.ExecuteQuery(query,search));
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] UserDto request, [FromServices] ICreateUserCommand command)
        {
            _executor.ExecuteCommand(command, request);
            return StatusCode(201);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDto request, [FromServices] IUpdateUserCommand command)
        {
            request.Id = id;
            _executor.ExecuteCommand(command, request);
            return NoContent();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,[FromServices] IDeleteUserCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
