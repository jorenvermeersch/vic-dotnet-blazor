using FluentValidation;
using Shared.Account;

namespace Client.Validation;

public class AccountValidation: AbstractValidator<AccountDto.Create>
{
    private readonly int _nameLength = 2;
    public AccountValidation()
	{
		RuleFor(x=>x.Firstname)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Voornaam")))
            .MinimumLength(_nameLength).WithMessage($"Voornaam heeft minstens {_nameLength} characters");
        RuleFor(x=>x.Lastname)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Naam")))
            .MinimumLength(_nameLength).WithMessage($"Naam heeft minstens {_nameLength} characters");
        RuleFor(x => x.IsActive)
            .NotEmpty();
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Email")));
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Wachtwoord")));
        RuleFor(x=>x.Role)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Rol")));
        RuleFor(x=>x.Department)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Departement")));
    }
}

