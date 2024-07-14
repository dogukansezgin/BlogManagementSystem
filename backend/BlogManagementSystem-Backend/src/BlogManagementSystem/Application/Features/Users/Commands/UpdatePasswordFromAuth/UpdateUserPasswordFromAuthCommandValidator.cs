using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Features.Users.Commands.UpdatePasswordFromAuth;

public class UpdateUserPasswordFromAuthCommandValidator : AbstractValidator<UpdateUserPasswordFromAuthCommand>
{
    public UpdateUserPasswordFromAuthCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserName).NotEmpty().MinimumLength(3);
        RuleFor(c => c.Password).NotEmpty().MinimumLength(6);
        RuleFor(c => c.NewPassword)
            .NotEmpty()
            .MinimumLength(6)
            .Must(StrongPassword)
            .WithMessage(
                "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character."
            );
    }

    private bool StrongPassword(string value)
    {
        Regex strongPasswordRegex = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled);

        return strongPasswordRegex.IsMatch(value);
    }
}
