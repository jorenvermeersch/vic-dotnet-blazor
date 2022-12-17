using FluentValidation;
using Shared.Hosts;

namespace Shared.Validation;

public class HostValidator : AbstractValidator<HostDto.Mutate>
{
    private readonly int _nameLength = 2;
    public HostValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.NotEmpty("Naam")))
            .MinimumLength(_nameLength).WithMessage($"Naam heeft minstens {_nameLength} characters");
        RuleFor(x => x.Specifications).SetValidator(new HostSpecificationsValidator());

    }
}
