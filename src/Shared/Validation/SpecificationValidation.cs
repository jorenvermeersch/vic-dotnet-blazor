using FluentValidation;
using Shared.Specifications;
using Shared.Validation;

namespace Shared.Validation;

[Obsolete]
public class SpecificationValidation : AbstractValidator<SpecificationsDto>
{
    private readonly int _minProcessorCount = 0;
    private readonly int _minMemoryCount = 0;
    private readonly int _minStorageCount = 0;

    public SpecificationValidation()
    {
        RuleFor(x => x.Processors)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("vCPU"))
            .GreaterThan(_minProcessorCount).WithMessage(FormMessages.GREATERTHAN(_minProcessorCount));
        RuleFor(x => x.Memory)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Geheugen"))
            .GreaterThan(_minProcessorCount).WithMessage(FormMessages.GREATERTHAN(_minMemoryCount));
        RuleFor(x => x.Storage)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Opslag"))
            .GreaterThan(_minProcessorCount).WithMessage(FormMessages.GREATERTHAN(_minStorageCount));
    }
}
