using FluentValidation;
using Shared.VirtualMachines;

namespace Shared.Validation;

[Obsolete]
public class VirtualMachineValidator : AbstractValidator<VirtualMachineDto.Mutate>
{
    private readonly int minNameLength = 2;
    private readonly int minFqdnLength = 2;
    private readonly int minReasonLength = 5;

    public VirtualMachineValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        // Configuration. 
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Naam"))
            .MinimumLength(minNameLength).WithMessage(ValidationMessages.MinimumLength("Naam", minNameLength));

        RuleFor(x => x.Fqdn)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("FQDN"))
            .MinimumLength(minFqdnLength).WithMessage(ValidationMessages.MinimumLength("FQDN", minFqdnLength));

        RuleFor(x => x.Mode)
            .NotNull().WithMessage(ValidationMessages.NotEmpty("Mode"))
            .IsInEnum().WithMessage(ValidationMessages.UnknownEnumValue("Mode", true));

        RuleFor(x => x.Template)
            .NotNull().WithMessage(ValidationMessages.NotEmpty("Template"))
            .IsInEnum().WithMessage(ValidationMessages.UnknownEnumValue("Template", true));

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Reden"))
            .MinimumLength(minReasonLength).WithMessage(ValidationMessages.MinimumLength("Reden", minReasonLength));

        RuleFor(x => x.Status)
            .NotNull().WithMessage(ValidationMessages.NotEmpty("Status"))
            .IsInEnum().WithMessage(ValidationMessages.UnknownEnumValue("Status", true));

        // Ports. 
        RuleFor(x => x.Ports)
            .NotEmpty().WithMessage("Virtuele machine moet bereikbaar zijn via minstens één poort.");

        RuleForEach(x => x.Ports)
            .GreaterThanOrEqualTo(1).WithMessage(ValidationMessages.GreaterThanOrEqual("Poortnummer", 1));

        // Specifications. 
        RuleFor(x => x.HostId)
            .GreaterThanOrEqualTo(1).WithMessage(ValidationMessages.NotEmpty("Host"));

        RuleFor(x => x.Specifications)
            .SetValidator(new SpecificationsValidator());

        // Availability. 
        RuleFor(x => x.ApplicationDate)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Aangevraagd op"))
            .LessThanOrEqualTo(x => x.StartDate).WithMessage("Aanvragingsdatum moet eerder of gelijk zijn aan startdatum.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Start"))
            .LessThan(x => x.EndDate).WithMessage("Startdatum moet eerder zijn dan einddatum");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Eind"))
            .GreaterThan(x => x.StartDate).WithMessage("Einddatum moet later zijn dan startdatum.");

        RuleFor(x => x.Availabilities)
            .NotEmpty().WithMessage("Virtuele machine moet beschikbaar zijn op minstens één dag.");

        RuleForEach(x => x.Availabilities)
            .IsInEnum().WithMessage(ValidationMessages.UnknownEnumValue("Beschikbaarheid", true));

        // Back-ups. 
        RuleFor(x => x.BackupFrequency)
            .NotNull().WithMessage(ValidationMessages.NotEmpty("Regelmaat"))
            .IsInEnum().WithMessage(ValidationMessages.UnknownEnumValue("Regelmaat", true));

        // Users. 
        RuleFor(x => x.RequesterId)
            .GreaterThanOrEqualTo(1).WithMessage(ValidationMessages.NotEmpty("Aanvrager"));

        RuleFor(x => x.UserId)
            .GreaterThanOrEqualTo(1).WithMessage(ValidationMessages.NotEmpty("Gebruiker"));

        RuleFor(x => x.AdministratorId)
            .GreaterThanOrEqualTo(1).WithMessage(ValidationMessages.NotEmpty("Beheerder"));

        // Credentials. 
        RuleFor(x => x.Credentials)
            .NotEmpty().WithMessage("Virtuele machine moet beschikbaar via minstens één paar logingegevens.");

        RuleForEach(x => x.Credentials).SetValidator(new CredentialsValidator());
    }
}
