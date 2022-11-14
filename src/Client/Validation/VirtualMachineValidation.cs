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
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Name"))
            .MinimumLength(_nameLenght).WithMessage(FormMessages.MINLENGTH(_nameLenght));
        RuleFor(x => x.FQDN)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("FQDN"))
            .MinimumLength(_fqdnLenght).WithMessage(FormMessages.MINLENGTH(_fqdnLenght));
        RuleFor(x => x.Mode)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Mode"));
        RuleFor(x => x.Template)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Template"));
        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Reason"))
            .MinimumLength(_reasonLenght).WithMessage(FormMessages.MINLENGTH(_reasonLenght));
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Status"));
        RuleFor(x => x.hostId)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Host"));
        RuleFor(x => x.Processors)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("vCPU"))
            .GreaterThan(_minProcessorCount).WithMessage(FormMessages.GREATERTHAN(_minProcessorCount));
        RuleFor(x => x.Memory)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Geheugen"))
            .GreaterThan(_minProcessorCount).WithMessage(FormMessages.GREATERTHAN(_minMemoryCount));
        RuleFor(x => x.Storage)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Opslag"))
            .GreaterThan(_minProcessorCount).WithMessage(FormMessages.GREATERTHAN(_minStorageCount));
        RuleFor(x => x.ApplicationDate)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Datum van aanvraag"));
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Startdatum"))
            .LessThan(x => x.EndDate).WithMessage(FormMessages.SMALLERTHANENDDATE());
        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Einddatum"))
            .GreaterThan(x => x.StartDate).WithMessage(FormMessages.GREATERTHANDATE());
        RuleFor(x => x.BackupFrequenty)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Regelmaat"));
        RuleFor(x => x.Requester)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Aanvrager"));
        RuleFor(x => x.User)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Gebruiker"));
        RuleFor(x => x.Account)
            .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Verantwoordelijke"));

    }
}
