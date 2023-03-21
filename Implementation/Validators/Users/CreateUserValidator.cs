using Application.DTO.Users;
using EfDataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Users
{
    public class CreateUserValidator : AbstractValidator<UserDto>
    {
        public CreateUserValidator(BookContext _context)
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").DependentRules(() =>
            {
                RuleFor(x => x.Email).Must(x => !_context.Users.Any(y => y.Email == x)).WithMessage(u => $"Email with an {u.Email} name already exists in database.");
            });
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Minimum lentgh for username is 3.")
            .MaximumLength(50).WithMessage("Maximum length for username is 50.")
            .DependentRules(() =>
            {
                RuleFor(x => x.UserName).Must(x => !_context.Users.Any(y => y.UserName == x)).WithMessage(u => $"Username with an {u.Email} name already exists in database.");
            });
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is requred.").MinimumLength(6).WithMessage("Minimum password length is 6.");
        }
    }
}
