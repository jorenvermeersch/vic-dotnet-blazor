using FluentValidation;
using Shared.Host;
using static Shared.Host.HostDto;

namespace Client.Validation;

public class HostValidation : AbstractValidator<HostDto.Create>
{
    private readonly int _nameLength = 2;
    public HostValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Name")))
            .MinimumLength(_nameLength).WithMessage($"Naam heeft minstens {_nameLength} characters");
        RuleFor(x => x.Specifications).SetValidator(new SpecificationValidation());

    }
}
