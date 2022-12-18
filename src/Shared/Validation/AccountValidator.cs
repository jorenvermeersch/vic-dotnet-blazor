using Domain.Constants;
using FluentValidation;
using Shared.Accounts;

namespace Shared.Validation;

public class AccountValidator : AbstractValidator<AccountDto.Mutate>
{
    public AccountValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Firstname)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Voornaam"))
            .Matches(ValidationRegex.Name).WithMessage(ValidationMessages.InvalidName("Voornaam"));

        RuleFor(x => x.Lastname)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Naam"))
            .Matches(ValidationRegex.Name).WithMessage(ValidationMessages.InvalidName("Naam"));

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Email"))
            .EmailAddress().WithMessage(ValidationMessages.InvalidEmailAddress);

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Rol"))
            .IsEnumName(typeof(Role)).WithMessage(ValidationMessages.UnknownRole);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Wachtwoord"))
            .Matches(ValidationRegex.Password).WithMessage(ValidationMessages.InvalidPassword);

        RuleFor(x => x.Department)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Departement"));

        RuleFor(x => x.Education)
            .Matches(ValidationRegex.Education).WithMessage(ValidationMessages.InvalidName("Opleiding"));
    }
}

