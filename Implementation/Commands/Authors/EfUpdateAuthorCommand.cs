using Application.Commands.Authors;
using Application.DTO.Authors;
using Application.Exceptions;
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
    public class EfUpdateAuthorCommand : EfCommand, IUpdateAuthorCommand
    {
        private readonly IMapper _mapper;
        private readonly UpdateAuthorValidator _validate;
        public EfUpdateAuthorCommand(BookContext context, UpdateAuthorValidator validate, IMapper mapper) : base(context)
        {
            _validate = validate;
            _mapper = mapper;
        }

        public IEnumerable<Role> Roles => new List<Role> { Role.Admin,Role.Moderator };

        public int Id => 2;

        public string Name => "Update author using Ef.";

        public void Execute(AuthorDto request)
        {
            var author = Context.Authors.Find(request.Id);
            if(author == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Author));
            }
             _validate.ValidateAndThrow(request);
            _mapper.Map(request, author);
            Context.SaveChanges();
        }
    }
}
