using FluentValidation;
using TestPlatform.Domain.Entities.Users;

namespace TestPlatform.Services.Validations.Users;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u)
            .NotNull()
            .NotEmpty();

        RuleFor(u => u.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
    }
}