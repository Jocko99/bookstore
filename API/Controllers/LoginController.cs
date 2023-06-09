﻿using API.Core;
using API.Jwt;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtManager _manager;

        public LoginController(JwtManager manager)
        {
            _manager = manager;
        }

        // POST api/<LoginController>
        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest request)
        {
            var token = _manager.MakeToken(request.Username, request.Password);
            if(token == null)
            {
                return Unauthorized();
            }
            return Ok(new { token });
        }

    }
}
