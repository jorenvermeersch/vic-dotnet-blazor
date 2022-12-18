using FluentValidation;
using Shared.VirtualMachines;

namespace Shared.Validation;

[Obsolete]
public class VirtualMachineValidator : AbstractValidator<VirtualMachineDto.Mutate>
{
    private readonly int minNameLength = 2;
    private readonly int minFqdnLength = 2;
    private readonly int minReasonLength = 5;
    private readonly int minProcessorCount = 0;
    private readonly int minMemory = 0;
    private readonly int minStorage = 0;

    public VirtualMachineValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Naam"))
            .MinimumLength(minNameLength).WithMessage(ValidationMessages.MinimumLength("Naam", minNameLength));

        RuleFor(x => x.Fqdn)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("FQDN"))
            .MinimumLength(minFqdnLength).WithMessage(ValidationMessages.MinimumLength("FQDN", minFqdnLength));

        RuleFor(x => x.Mode)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Mode"))
            .IsInEnum().WithMessage(ValidationMessages.UnknownEnumValue("Mode", true));

        RuleFor(x => x.Template)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Template"))
            .IsInEnum().WithMessage(ValidationMessages.UnknownEnumValue("Template", true));

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Reden"))
            .MinimumLength(minReasonLength).WithMessage(ValidationMessages.MinimumLength("Reden", minReasonLength));

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Status"))
            .IsInEnum().WithMessage(ValidationMessages.UnknownEnumValue("Status", true));


        RuleFor(x => x.ApplicationDate)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Aangevraagd op"));

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Start"))
            .LessThan(x => x.EndDate).WithMessage(ValidationMessages.SMALLER_THAN_END_DATE());

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Eind"))
            .GreaterThan(x => x.StartDate).WithMessage(ValidationMessages.GREATER_THAN_DATE());

        RuleFor(x => x.BackupFrequency)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Regelmaat"))
            .IsInEnum().WithMessage(ValidationMessages.UnknownEnumValue("Regelmaat", true));

        RuleFor(x => x.RequesterId)
            .GreaterThanOrEqualTo(1).WithMessage(ValidationMessages.NotEmpty("Aanvrager"));

        RuleFor(x => x.UserId)
            .GreaterThanOrEqualTo(1).WithMessage(ValidationMessages.NotEmpty("Gebruiker"));

        RuleFor(x => x.AdministratorId)
            .GreaterThanOrEqualTo(1).WithMessage(ValidationMessages.NotEmpty("Beheerder"));

        RuleFor(x => x.HostId)
            .GreaterThanOrEqualTo(1).WithMessage(ValidationMessages.NotEmpty("Host"));
    }
}
