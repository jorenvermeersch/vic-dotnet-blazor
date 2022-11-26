using FluentValidation;
using Shared.Validation;
using Shared.VirtualMachines;

namespace Shared.Validation;

public class CredentialValidation : AbstractValidator<CredentialsDto>
{
    private readonly int _nameLength = 2;
    public CredentialValidation()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Username")))
            .MinimumLength(_nameLength).WithMessage($"Naam heeft minstens {_nameLength} characters");
        RuleFor(x => x.PasswordHash)
            .Matches(Validation.Password).WithMessage(string.Format(FormMessages.INVALIDPASSWORD()))
            .NotEmpty().WithMessage("Geef een waarde in");
        RuleFor(x => x.Role)
           .NotEmpty().WithMessage("Geef een waarde in");
    }
}
