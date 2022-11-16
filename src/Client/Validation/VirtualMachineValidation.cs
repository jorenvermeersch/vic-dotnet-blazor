using FluentValidation;
using Shared.Host;
using Shared.VirtualMachine;
using static Shared.Host.HostDto;

namespace Client.Validation;

public class VirtualMachineValidation : AbstractValidator<VirtualMachineDto.Create>
{
    private readonly int _nameLenght = 2;
    private readonly int _fqdnLenght = 2;
    private readonly int _reasonLenght = 5;
    private readonly int _minProcessorCount = 0;
    private readonly int _minMemoryCount = 0;
    private readonly int _minStorageCount = 0;

    public VirtualMachineValidation()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Name"))
            .MinimumLength(_nameLenght).WithMessage(FormMessages.MINLENGTH(_nameLenght));
        RuleFor(x => x.FQDN)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .Matches("\"^(?!:\\\\/\\\\/)(?=.{1,255}$)((.{1,63}\\\\.){1,127}(?![0-9]*$)[a-z0-9-]+\\\\.?)$\"").WithMessage(string.Format("Dit is geen geldige FQDN."))
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
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Reason"))
            .MinimumLength(_reasonLenght).WithMessage(FormMessages.MINLENGTH(_reasonLenght));
        RuleFor(x => x.Status)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Status"));
        //RuleFor(x => x.hostId)
        //    .Cascade(CascadeMode.StopOnFirstFailure)
        //    .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Host"));
        RuleFor(x => x.Processors)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("vCPU"))
            .GreaterThan(_minProcessorCount).WithMessage(FormMessages.GREATERTHAN(_minProcessorCount));
        RuleFor(x => x.Memory)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Geheugen"))
            .GreaterThan(_minProcessorCount).WithMessage(FormMessages.GREATERTHAN(_minMemoryCount));
        RuleFor(x => x.Storage)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Opslag"))
            .GreaterThan(_minProcessorCount).WithMessage(FormMessages.GREATERTHAN(_minStorageCount));
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
        RuleFor(x => x.BackupFrequenty)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Regelmaat"));
        RuleFor(x => x.Requester)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Aanvrager"));
        RuleFor(x => x.User)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Gebruiker"));
        RuleFor(x => x.Account)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Verantwoordelijke"));

    }
}
