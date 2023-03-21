using Application.DTO.Genres;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Genres
{
    public class CreateGenreValidator : AbstractValidator<GenreDto>
    {
        public CreateGenreValidator(BookContext _context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Genre name is requred.").DependentRules(() =>
            {
                RuleFor(x => x.Name).Must(x => !_context.Genres.Any(y => y.Name == x)).WithMessage(c => $"Genre with an {c.Name} name already exists in database.");
            });
        }
    }
}
