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
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Mode"));

        RuleFor(x => x.Template)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Template"));

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Reden"))
            .MinimumLength(minReasonLength).WithMessage(ValidationMessages.MinimumLength("Reden", minReasonLength));

        RuleFor(x => x.Status)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Status"));

        //RuleFor(x => x.hostId)
        //    .Cascade(CascadeMode.StopOnFirstFailure)
        //    .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Host"));

        RuleFor(x => x.ApplicationDate)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Datum van aanvraag"));

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Startdatum"))
            .LessThan(x => x.EndDate).WithMessage(ValidationMessages.SMALLER_THAN_END_DATE());

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Einddatum"))
            .GreaterThan(x => x.StartDate).WithMessage(ValidationMessages.GREATER_THAN_DATE());

        RuleFor(x => x.BackupFrequency)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Regelmaat"));

        // TODO: Not objects, but Id of request, user and account are given!
        RuleFor(x => x.RequesterId)
            .GreaterThan(0).WithMessage(ValidationMessages.NotEmpty("Aanvrager"));

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage(ValidationMessages.NotEmpty("Gebruiker"));

        RuleFor(x => x.AdministratorId)
            .GreaterThan(0).WithMessage(ValidationMessages.NotEmpty("Verantwoordelijke"));

        RuleFor(x => x.HostId)
            .GreaterThan(0).WithMessage(ValidationMessages.NotEmpty("Host"));

        //RuleFor(x => x.UserId)
        //    .Cascade(CascadeMode.StopOnFirstFailure)
        //    .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Gebruiker"));
        //RuleFor(x => x.AdministratorId)
        //    .Cascade(CascadeMode.StopOnFirstFailure)
        //    .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Verantwoordelijke"));
        //RuleFor(x => x.Specifications).SetValidator(new SpecificationValidation());
    }
}
