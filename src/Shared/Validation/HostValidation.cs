using FluentValidation;
using Shared.Host;
using Shared.Validation;

namespace Shared.Validation;

public class HostValidation : AbstractValidator<HostDto.Mutate>
{
    private readonly int _nameLength = 2;
    public HostValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Naam")))
            .MinimumLength(_nameLength).WithMessage($"Naam heeft minstens {_nameLength} characters");
        RuleFor(x => x.Specifications).SetValidator(new SpecificationValidation());

    }
}
