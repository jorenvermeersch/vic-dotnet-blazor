using FluentValidation;
using Shared.Account;
using Shared.Validation;

namespace Client.Validation;

public class AccountValidation : AbstractValidator<AccountDto.Create>
{
    private readonly int _nameLength = 2;
    public AccountValidation()
    {
        RuleFor(x => x.Firstname)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Voornaam")))
            .Matches("^[a-zA-Z]{2}[- a-zA-Zéèçàùëüöï]*$").WithMessage(string.Format(FormMessages.INVALIDNAME("Voornaam")))
            .MinimumLength(_nameLength).WithMessage($"Voornaam heeft minstens {_nameLength} characters");
        RuleFor(x => x.Lastname)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Naam")))
            .Matches("^[a-zA-Z]{2}[- a-zA-Zéèçàùëüöï]*$").WithMessage(string.Format(FormMessages.INVALIDNAME("Voornaam")))
            .MinimumLength(_nameLength).WithMessage($"Naam heeft minstens {_nameLength} characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Email")))
            .EmailAddress().WithMessage(string.Format(FormMessages.INVALIDEMAIL()));
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Wachtwoord")))
            .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").WithMessage(string.Format(FormMessages.INVALIDPASSWORD()));
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Rol")));
        RuleFor(x => x.Department)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Departement")));
    }
}

