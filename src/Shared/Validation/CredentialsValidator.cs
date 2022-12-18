using FluentValidation;
using Shared.VirtualMachines;

namespace Shared.Validation;

public class CredentialsValidator : AbstractValidator<CredentialsDto>
{
    private readonly int minUsernameLength = 2;
    public CredentialsValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.NotEmpty("Gebruikersnaam")))
            .MinimumLength(minUsernameLength).WithMessage(ValidationMessages.MinimumLength("Gebruikersnaam", minUsernameLength));

        RuleFor(x => x.PasswordHash)
            .Matches(ValidationRegex.Password).WithMessage(ValidationMessages.InvalidPassword)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Wachtwoord"));

        RuleFor(x => x.Role)
           .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Rol"));
    }
}
