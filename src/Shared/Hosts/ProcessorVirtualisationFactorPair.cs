using FluentValidation;
using Shared.Validation;

namespace Shared.Hosts;

public class ProcessorVirtualisationFactorPair
{
    public ProcessorDto? Processor { get; set; }
    public int VirtualisationFactor { get; set; }

    public class ProcessorVirtualisationFactorPairValidator : AbstractValidator<ProcessorVirtualisationFactorPair>
    {
        private readonly int minVirtualisationFactor = 1;
        public ProcessorVirtualisationFactorPairValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Processor)
                .NotEmpty()
                .WithMessage(ValidationMessages.NotEmpty("Type"));

            RuleFor(x => x.VirtualisationFactor)
                .GreaterThanOrEqualTo(minVirtualisationFactor)
                .WithMessage(ValidationMessages.GreaterThanOrEqual("Virtualisatiefactor", minVirtualisationFactor));
        }
    }
}
