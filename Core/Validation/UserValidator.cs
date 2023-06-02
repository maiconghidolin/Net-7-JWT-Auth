using Domain.Entities;
using FluentValidation;

namespace Core.Validation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Invalid Name");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Invalid Email");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Invalid Password");
    }
}
