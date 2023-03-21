using Application.DTO.Books;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Books
{
    public class BookAuthorValidator : AbstractValidator<BookAuthorDto>
    {
        public BookAuthorValidator(BookContext _context)
        {
            RuleFor(x => x.AuthorId).Must(x => _context.Authors.Any(y => y.Id == x)).WithMessage("Author doesn't exists.");
        }
    }
}
