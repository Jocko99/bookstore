using Application.Commands.Authors;
using Application.DTO.Authors;
using AutoMapper;
using Domain.Entities;
using EfDataAccess;
using FluentValidation;
using Implementation.Validators.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Authors
{
    public class EfCreateAuthorCommand : EfCommand, ICreateAuthorCommand
    {
        private readonly IMapper _mapper;
        private readonly CreateAuthorValidator _validate;

        public EfCreateAuthorCommand(BookContext context, CreateAuthorValidator validate, IMapper mapper) : base(context)
        {
            _validate = validate;
            _mapper = mapper;
        }

        public IEnumerable<Role> Roles => new List<Role> { Role.Admin, Role.Moderator };

        public int Id => 1;

        public string Name => "Create Author using Ef.";

        public void Execute(AuthorDto request)
        {
            _validate.ValidateAndThrow(request);
            Context.Authors.Add(_mapper.Map<Author>(request));
            Context.SaveChanges();
        }
    }
}
