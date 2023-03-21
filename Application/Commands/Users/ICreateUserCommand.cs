﻿using Application.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users
{
    public interface ICreateUserCommand : ICommand<UserDto>
    {
    }
}
