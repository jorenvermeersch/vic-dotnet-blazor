using FluentValidation;
using Shared.Accounts;

namespace Shared.Validation;

public class AccountValidator : AbstractValidator<AccountDto.Mutate>
{
    public AccountValidator()
    {
        RuleFor(x => x.Firstname)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Voornaam")))
            .Matches(ValidationRegex.Name).WithMessage(string.Format(FormMessages.INVALIDNAME("Voornaam")));
        RuleFor(x => x.Lastname)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Naam")))
            .Matches(ValidationRegex.Name).WithMessage(string.Format(FormMessages.INVALIDNAME("Naam")));
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Email")))
            .EmailAddress().WithMessage(string.Format(FormMessages.INVALIDEMAIL()));
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Rol")));
        //RuleFor(x => x.Password)
        //    .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Wachtwoord")))
        //    .Matches(ValidationRegex.Password).WithMessage(string.Format(FormMessages.INVALIDPASSWORD()));
        RuleFor(x => x.Department)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Departement")));
        RuleFor(x => x.Education)
            .Matches(ValidationRegex.Education).WithMessage(string.Format(FormMessages.INVALIDNAME("Opleiding")));
    }
}

