using FluentValidation;
using Shared.VirtualMachines;

namespace Shared.Validation;
public class SpecificationsValidator : AbstractValidator<SpecificationsDto>
{
    private readonly int minProcessorCount = 1;
    private readonly int minMemory = 1;
    private readonly int minStorage = 1;

    public SpecificationsValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.VirtualProcessors)
            .NotNull().WithMessage(ValidationMessages.NotEmpty("vCPUs"))
            .GreaterThanOrEqualTo(minProcessorCount).WithMessage(ValidationMessages.GreaterThanOrEqual("Processoren", minProcessorCount));

        RuleFor(x => x.Memory)
            .NotNull().WithMessage(ValidationMessages.NotEmpty("Geheugen"))
            .GreaterThanOrEqualTo(minProcessorCount).WithMessage(ValidationMessages.GreaterThanOrEqual("Geheugen", minMemory, "GB"));

        RuleFor(x => x.Storage)
            .NotNull().WithMessage(ValidationMessages.NotEmpty("Opslag"))
            .GreaterThanOrEqualTo(minProcessorCount).WithMessage(ValidationMessages.GreaterThanOrEqual("Opslag", minStorage, "GB"));
    }
}
