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
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Naam"))
            .MinimumLength(_nameLenght).WithMessage(FormMessages.MINLENGTH(_nameLenght));
        RuleFor(x => x.Fqdn)
            .Cascade(CascadeMode.StopOnFirstFailure)
            //.Matches("\"^(?!:\\\\/\\\\/)(?=.{1,255}$)((.{1,63}\\\\.){1,127}(?![0-9]*$)[a-z0-9-]+\\\\.?)$\"").WithMessage(string.Format("Dit is geen geldige FQDN."))
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("FQDN"))
            .MinimumLength(_fqdnLenght).WithMessage(FormMessages.MINLENGTH(_fqdnLenght));
        RuleFor(x => x.Mode)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Mode"));
        RuleFor(x => x.Template)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Template"));
        RuleFor(x => x.Reason)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Reden"))
            .MinimumLength(_reasonLenght).WithMessage(FormMessages.MINLENGTH(_reasonLenght));
        RuleFor(x => x.Status)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Status"));
        //RuleFor(x => x.hostId)
        //    .Cascade(CascadeMode.StopOnFirstFailure)
        //    .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Host"));
        RuleFor(x => x.ApplicationDate)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Datum van aanvraag"));
        RuleFor(x => x.StartDate)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Startdatum"))
            .LessThan(x => x.EndDate).WithMessage(FormMessages.SMALLERTHANENDDATE());
        RuleFor(x => x.EndDate)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Einddatum"))
            .GreaterThan(x => x.StartDate).WithMessage(FormMessages.GREATERTHANDATE());
        RuleFor(x => x.BackupFrequency)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Regelmaat"));

        // TODO: Not objects, but Id of request, user and account are given!
        /*        RuleFor(x => x.Requester)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Aanvrager"));
                RuleFor(x => x.User)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Gebruiker"));
                RuleFor(x => x.Account)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Verantwoordelijke"));
                RuleFor(x => x.Specifications).SetValidator(new SpecificationValidation());*/
    }
}
