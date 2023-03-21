﻿using Application;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Jwt
{
    public class JwtActor : IApplicationActor
    {
        public int Id { get; set; }

        public string Identity { get; set; }

        public Role Role { get; set; }

    }
}
