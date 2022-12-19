using FluentValidation;
using Shared.Hosts;

namespace Shared.Validation;

public class HostValidator : AbstractValidator<HostDto.Mutate>
{
    private readonly int minNameLength = 3;
    public HostValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.NotEmpty("Naam")))
            .MinimumLength(minNameLength).WithMessage(ValidationMessages.MinimumLength("Naam", minNameLength));

        RuleFor(x => x.Specifications).SetValidator(new HostSpecificationsValidator());

    }
}
