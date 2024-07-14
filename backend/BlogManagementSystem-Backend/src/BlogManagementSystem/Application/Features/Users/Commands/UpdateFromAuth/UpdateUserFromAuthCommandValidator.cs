using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Features.Users.Commands.UpdateFromAuth;

public class UpdateUserFromAuthCommandValidator : AbstractValidator<UpdateUserFromAuthCommand>
{
    public UpdateUserFromAuthCommandValidator()
    {
        RuleFor(c => c.UserName).NotEmpty().MinimumLength(3);
        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress()
            .Must(BeValidEmail)
            .WithMessage("Email must be a Gmail or Hotmail address.");
    }

    private bool BeValidEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex emailRegex = new(emailPattern, RegexOptions.Compiled);

        if (!emailRegex.IsMatch(email))
        {
            return false;
        }

        if (email.Count(c => c == '@') != 1)
        {
            return false;
        }

        string[] validDomains = { "gmail.com", "hotmail.com" };
        string emailDomain = email.Split('@').Last();

        return validDomains.Contains(emailDomain);
    }
}
