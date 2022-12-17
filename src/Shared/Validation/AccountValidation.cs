using FluentValidation;
using Shared.Accounts;

namespace Shared.Validation;

public class AccountValidator : AbstractValidator<AccountDto.Mutate>
{
    public AccountValidator()
    {
        RuleFor(x => x.Firstname)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.NotEmpty("Voornaam")))
            .Matches(ValidationRegex.Name).WithMessage(string.Format(ValidationMessages.INVALID_NAME("Voornaam")));
        RuleFor(x => x.Lastname)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.NotEmpty("Naam")))
            .Matches(ValidationRegex.Name).WithMessage(string.Format(ValidationMessages.INVALID_NAME("Naam")));
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.NotEmpty("Email")))
            .EmailAddress().WithMessage(string.Format(ValidationMessages.INVALID_EMAIL()));
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.NotEmpty("Rol")));
        //RuleFor(x => x.Password)
        //    .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Wachtwoord")))
        //    .Matches(ValidationRegex.Password).WithMessage(string.Format(FormMessages.INVALIDPASSWORD()));
        RuleFor(x => x.Department)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.NotEmpty("Departement")));
        RuleFor(x => x.Education)
            .Matches(ValidationRegex.Education).WithMessage(string.Format(ValidationMessages.INVALID_NAME("Opleiding")));
    }
}

