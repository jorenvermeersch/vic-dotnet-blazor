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
            .NotEmpty().WithMessage("Geef de host een naam")
            .MinimumLength(_nameLength).WithMessage($"Naam heeft minstens {_nameLength} characters");
        RuleFor(x => x.Processors)
            .NotEmpty().WithMessage("Geef een waarde in")
            .GreaterThan(0).WithMessage("De waarde kan niet onder 0 zijn");
        RuleFor(x => x.Storage)
           .NotEmpty().WithMessage("Geef een waarde in")
           .GreaterThan(0).WithMessage("De waarde kan niet onder 0 zijn");
        RuleFor(x => x.Memory)
            .NotEmpty().WithMessage("Geef een waarde in")
            .GreaterThan(0).WithMessage("De waarde kan niet onder 0 zijn");

    }
}
