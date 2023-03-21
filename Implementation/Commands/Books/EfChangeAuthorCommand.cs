using Application.Commands.Books;
using Application.DTO.Books;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using EfDataAccess;
using FluentValidation;
using Implementation.Validators.Books;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Books
{
    public class EfChangeAuthorCommand : EfCommand, IChangeAuthorCommand
    {
        private readonly BookAuthorValidator _validate;
        private readonly IMapper _mapper;
        public EfChangeAuthorCommand(BookContext context, BookAuthorValidator validate, IMapper mapper) : base(context)
        {
            _validate = validate;
            _mapper = mapper;
        }

        public IEnumerable<Role> Roles => new List<Role> { Role.Admin, Role.Moderator };

        public int Id => 15;

        public string Name => "Change authors using Ef.";

        public void Execute(BookAuthorDto request)
        {
            var book = Context.BookAuthors.Where(x => x.BookId == request.BookId);
            if(book == null)
            {
                throw new EntityNotFoundException(request.BookId, typeof(Book));
            }
            _validate.ValidateAndThrow(request);
            Context.BookAuthors.RemoveRange(book);
            Context.BookAuthors.Add(_mapper.Map<BookAuthor>(request));
            Context.SaveChanges();
        }
    }
}
