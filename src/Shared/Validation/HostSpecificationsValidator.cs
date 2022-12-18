using FluentValidation;
using Shared.Hosts;

namespace Shared.Validation;

public class HostSpecificationsValidator : AbstractValidator<HostSpecificationsDto>
{
    private readonly int minStorage = 1;
    private readonly int minMemory = 1;

    public HostSpecificationsValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Storage)
            .GreaterThanOrEqualTo(minStorage).WithMessage(ValidationMessages.GreaterThanOrEqual("Opslag", minStorage, "GB"));

        RuleFor(x => x.Memory)
            .GreaterThanOrEqualTo(minMemory).WithMessage(ValidationMessages.GreaterThanOrEqual("Geheugen", minMemory, "GB"));

        RuleFor(x => x.Processors).NotEmpty().WithMessage(ValidationMessages.NotEmpty("Aantal"));
    }
}
