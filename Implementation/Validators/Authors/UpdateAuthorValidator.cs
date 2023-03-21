using Application.DTO.Authors;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Authors
{
    public class UpdateAuthorValidator : AbstractValidator<AuthorDto>
    {
        public UpdateAuthorValidator(BookContext _context)
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Author name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Author surname is required.");
            RuleFor(x => x.Biography).NotEmpty().WithMessage("Biography is required.");
            RuleFor(x => x.CoverPhoto).NotEmpty().WithMessage("Photo is required.").DependentRules(() =>
            {
                RuleFor(x => x.CoverPhoto).Must((dto, x) => !_context.Authors.Any(y => y.CoverPhoto == x && dto.Id != y.Id)).WithMessage("Photo already exists in database.");
            });
        }
    }
}
