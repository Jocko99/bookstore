﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public class AppSettings
    {
        public string JwtSecretKey { get; set; }
        public string JwtIssuer { get; set; }
    }
}
