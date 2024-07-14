using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(c => c.UserForRegisterDto.UserName).NotEmpty().MinimumLength(3);
        RuleFor(c => c.UserForRegisterDto.Email)
            .NotEmpty()
            .EmailAddress()
            .Must(BeValidEmail)
            .WithMessage("Email must be a Gmail or Hotmail address.");
        RuleFor(c => c.UserForRegisterDto.Password)
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
