using Application.Commands.Users;
using Application.DTO.Users;
using Application.Exceptions;
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
    public class EfUpdateUserCommand : EfCommand, IUpdateUserCommand
    {
        private readonly UpdateUserValidator _validate;
        private readonly IMapper _mapper;
        public EfUpdateUserCommand(BookContext context, UpdateUserValidator validate, IMapper mapper) : base(context)
        {
            _validate = validate;
            _mapper = mapper;
        }

        public IEnumerable<Role> Roles => new List<Role> { Role.Admin };

        public int Id => 10;

        public string Name => "Edit user using Ef.";

        public void Execute(UserDto request)
        {
            var user = Context.Users.Find(request.Id);
            if(user == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(User));
            }
            _validate.ValidateAndThrow(request);
            _mapper.Map(request, user);
            Context.SaveChanges();
        }
    }
}
