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
    public class BookUpdateValidator : AbstractValidator<BookDto>
    {
        public BookUpdateValidator(BookContext _context)
        {
            RuleFor(x => x.ISBN).NotEmpty().WithMessage("ISBN is required.").Length(13).WithMessage("ISBN need to have 13 characters").DependentRules(() =>
            {
                RuleFor(x => x.ISBN).Must((dto, x) => !_context.Books.Any(y => y.ISBN == x && y.Id != dto.Id)).WithMessage(b => $"ISBN with an {b.ISBN} name already exists in database.");
            });
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.").DependentRules(() =>
            {
                RuleFor(x => x.Title).Must((dto, x) => !_context.Books.Any(y => y.Title == x && y.Id != dto.Id)).WithMessage(b => $"Title with an {b.Title} name already exists in database.");
            });
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.GenreId).NotEmpty().WithMessage("Genre is required").DependentRules(() =>
            {
                RuleFor(x => x.GenreId).Must((dto, x) => _context.Genres.Any(y => y.Id == x && y.Id != dto.Id)).WithMessage("Selected genre doesn't exists.");
            });
            RuleFor(x => x.Stock).NotEmpty().WithMessage("Stock is required").GreaterThanOrEqualTo(0).WithMessage("Stock can't be less then 0.");
            RuleFor(x => x.UnitPrice).NotEmpty().WithMessage("Price is required.").GreaterThanOrEqualTo(0).WithMessage("Price can't be less then 0.");
            RuleFor(x => x.ReleaseDate).NotEmpty().WithMessage("Release date is required.");
        }
    }
}
