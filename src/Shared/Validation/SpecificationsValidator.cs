using FluentValidation;
using Shared.VirtualMachines;

namespace Shared.Validation;

[Obsolete]
public class SpecificationsValidator : AbstractValidator<SpecificationsDto>
{
    private readonly int _minProcessorCount = 0;
    private readonly int _minMemoryCount = 0;
    private readonly int _minStorageCount = 0;

    public SpecificationsValidator()
    {
        RuleFor(x => x.VirtualProcessors)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("vCPU"))
            .GreaterThan(_minProcessorCount).WithMessage(ValidationMessages.GreaterThanOrEqual("Processoren", _minProcessorCount));
        RuleFor(x => x.Memory)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Geheugen"))
            .GreaterThan(_minProcessorCount).WithMessage(ValidationMessages.GreaterThanOrEqual("Geheugen", _minMemoryCount));
        RuleFor(x => x.Storage)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Opslag"))
            .GreaterThan(_minProcessorCount).WithMessage(ValidationMessages.GreaterThanOrEqual("Opslag", _minStorageCount));
    }
}
