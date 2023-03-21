using Application.Commands.Users;
using Application.DTO.Users;
using AutoMapper;
using Domain.Entities;
using EfDataAccess;
using FluentValidation;
using Implementation.Validators.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Users
{
    public class EfCreateUserCommand : EfCommand, ICreateUserCommand
    {
        private readonly CreateUserValidator _validate;
        private readonly IMapper _mapper;
        public EfCreateUserCommand(BookContext context, CreateUserValidator validate, IMapper mapper) : base(context)
        {
            _validate = validate;
            _mapper = mapper;
        }

        public IEnumerable<Role> Roles => new List<Role> { Role.Admin };

        public int Id => 9;

        public string Name => "Create user using Ef.";

        public void Execute(UserDto request)
        {
            _validate.ValidateAndThrow(request);
            Context.Users.Add(_mapper.Map<User>(request));
            Context.SaveChanges();
        }
    }
}
