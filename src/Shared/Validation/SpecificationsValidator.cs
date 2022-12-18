using FluentValidation;
using Shared.VirtualMachines;

namespace Shared.Validation;

[Obsolete]
public class SpecificationsValidator : AbstractValidator<SpecificationsDto>
{
    private readonly int minProcessorCount = 1;
    private readonly int minMemory = 1;
    private readonly int minStorage = 1;

    public SpecificationsValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.VirtualProcessors)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("vCPU"))
            .GreaterThanOrEqualTo(minProcessorCount).WithMessage(ValidationMessages.GreaterThanOrEqual("Processoren", minProcessorCount));

        RuleFor(x => x.Memory)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Geheugen"))
            .GreaterThanOrEqualTo(minProcessorCount).WithMessage(ValidationMessages.GreaterThanOrEqual("Geheugen", minMemory));

        RuleFor(x => x.Storage)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Opslag"))
            .GreaterThanOrEqualTo(minProcessorCount).WithMessage(ValidationMessages.GreaterThanOrEqual("Opslag", minStorage));
    }
}
