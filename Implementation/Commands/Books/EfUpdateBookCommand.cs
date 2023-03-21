using Application.Commands.Books;
using Application.DTO.Books;
using Application.Exceptions;
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
    public class EfUpdateBookCommand : EfCommand, IUpdateBookCommand
    {
        private readonly BookUpdateValidator _validate;
        private readonly IMapper _mapper;
        public EfUpdateBookCommand(BookContext context, BookUpdateValidator validate, IMapper mapper) : base(context)
        {
            _validate = validate;
            _mapper = mapper;
        }

        public IEnumerable<Role> Roles => new List<Role> { Role.Admin, Role.Moderator };

        public int Id => 14;

        public string Name => "Update book using Ef.";

        public void Execute(BookDto request)
        {
            var book = Context.Books.Find(request.Id);
            if (book == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Book));
            }
            _validate.ValidateAndThrow(request);
            _mapper.Map(request, book);
            Context.SaveChanges();
        }
    }
}
