using Application.Commands.Books;
using Application.DTO.Books;
using AutoMapper;
using Domain.Entities;
using EfDataAccess;
using FluentValidation;
using Implementation.Validators.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Books
{
    public class EfCreateBookCommand : EfCommand, ICreateBookCommand
    {
        private readonly CreateBookValidator _validate;
        private readonly IMapper _mapper;

        public EfCreateBookCommand(BookContext context, CreateBookValidator validate, IMapper mapper) : base(context)
        {
            _validate = validate;
            _mapper = mapper;
        }

        public IEnumerable<Role> Roles => new List<Role> { Role.Admin, Role.Moderator };

        public int Id => 13;

        public string Name => "Create book using Ef.";

        public void Execute(BookCreateDto request)
        {
            _validate.ValidateAndThrow(request);
            Context.Books.Add(_mapper.Map<Book>(request));
            Context.SaveChanges();
        }
    }
}
