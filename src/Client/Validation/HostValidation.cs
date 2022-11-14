using FluentValidation;
using Shared.Host;
using static Shared.Host.HostDto;

namespace Client.Validation;

public class HostValidation : AbstractValidator<HostDto.Create>
{
    public HostValidation()
    {
        RuleFor(x => x.Name).Equal("Nee").WithMessage("Vul veld naam in");
        RuleFor(x => x.Processors).NotEmpty().WithMessage("please enter some values");
    }
}
