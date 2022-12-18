using FluentValidation;
using Shared.VirtualMachines;

namespace Shared.Validation;

[Obsolete]
public class VirtualMachineValidator : AbstractValidator<VirtualMachineDto.Mutate>
{
    private readonly int _nameLenght = 2;
    private readonly int _fqdnLenght = 2;
    private readonly int _reasonLenght = 5;
    private readonly int _minProcessorCount = 0;
    private readonly int _minMemoryCount = 0;
    private readonly int _minStorageCount = 0;

    public VirtualMachineValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Naam"))
            .MinimumLength(_nameLenght).WithMessage(ValidationMessages.MinimumLength("Naam", _nameLenght));

        RuleFor(x => x.Fqdn)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("FQDN"))
            .MinimumLength(_fqdnLenght).WithMessage(ValidationMessages.MinimumLength("FQDN", _fqdnLenght));

        RuleFor(x => x.Mode)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Mode"));

        RuleFor(x => x.Template)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Template"));

        RuleFor(x => x.Reason)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Reden"))
            .MinimumLength(_reasonLenght).WithMessage(ValidationMessages.MinimumLength("Reden", _reasonLenght));

        RuleFor(x => x.Status)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Status"));

        //RuleFor(x => x.hostId)
        //    .Cascade(CascadeMode.StopOnFirstFailure)
        //    .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Host"));

        RuleFor(x => x.ApplicationDate)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Datum van aanvraag"));

        RuleFor(x => x.StartDate)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Startdatum"))
            .LessThan(x => x.EndDate).WithMessage(ValidationMessages.SMALLER_THAN_END_DATE());

        RuleFor(x => x.EndDate)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Einddatum"))
            .GreaterThan(x => x.StartDate).WithMessage(ValidationMessages.GREATER_THAN_DATE());

        RuleFor(x => x.BackupFrequency)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Regelmaat"));

        // TODO: Not objects, but Id of request, user and account are given!
        RuleFor(x => x.RequesterId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .GreaterThan(0).WithMessage(ValidationMessages.NotEmpty("Aanvrager"));
        RuleFor(x => x.UserId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .GreaterThan(0).WithMessage(ValidationMessages.NotEmpty("Gebruiker"));
        RuleFor(x => x.AdministratorId)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .GreaterThan(0).WithMessage(ValidationMessages.NotEmpty("Verantwoordelijke"));
        RuleFor(x => x.HostId)
            .Cascade(CascadeMode.StopOnFirstFailure)
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
