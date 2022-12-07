using FluentValidation;
using Shared.Hosts;

namespace Shared.Validation;

public class HostSpecificationsValidator : AbstractValidator<HostSpecificationsDto>
{
    public HostSpecificationsValidator()
    {
        RuleFor(x=>x.Storage)
            .GreaterThan(10).WithMessage(FormMessages.GREATERTHAN(10));
        RuleFor(x => x.Memory)
            .GreaterThan(10).WithMessage(FormMessages.GREATERTHAN(10));
        RuleFor(x => x.Processors).NotNull().WithMessage(FormMessages.NOTEMPTY("Aantal"));
    }
}
