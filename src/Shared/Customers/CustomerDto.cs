using Domain.Constants;
using FluentValidation;
using Shared.Validation;
using Shared.VirtualMachines;

namespace Shared.Customers;

public static class CustomerDto
{
    public class Index
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public CustomerType CustomerType { get; set; }
        public string Email { get; set; } = default!;
    }

    public class Detail
    {
        public long Id { get; set; }
        public Institution? Institution { get; set; }
        public string? Department { get; set; }
        public string? Education { get; set; }
        public CustomerType CustomerType { get; set; }
        public string? CompanyType { get; set; }
        public string? CompanyName { get; set; }
        public ContactPersonDto ContactPerson { get; set; } = default!;
        public ContactPersonDto? BackupContactPerson { get; set; }
        public List<VirtualMachineDto.Index> VirtualMachines { get; set; } = new();
    }
    public class Mutate
    {
        public string CustomerType { get; set; } = default!;
        public string? Institution { get; set; }
        public string? Department { get; set; }
        public string? CompanyType { get; set; }
        public string? Education { get; set; }
        public string? CompanyName { get; set; }
        public ContactPersonDto ContactPerson { get; set; } = new();
        public ContactPersonDto? BackupContactPerson { get; set; } = new();

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.CustomerType)
                    .NotEmpty().WithMessage(FormMessages.NOTEMPTY("Type klant"));
                RuleFor(x => x.CompanyName)
                    .NotEmpty().When(customer => customer.CustomerType == "Extern").WithMessage(FormMessages.NOTEMPTY("Naam"));
                RuleFor(x => x.CompanyType)
                    .NotEmpty().When(customer => customer.CustomerType == "Extern").WithMessage(FormMessages.NOTEMPTY("Type extern"));
                RuleFor(x => x.Department)
                    .NotEmpty().When(customer => customer.CustomerType == "Intern").WithMessage(FormMessages.NOTEMPTY("Departement"));
                RuleFor(x => x.Institution)
                    .NotEmpty().When(customer => customer.CustomerType == "Intern").WithMessage(FormMessages.NOTEMPTY("Institutie"));
                
                RuleFor(x => x.ContactPerson)
                    .NotEmpty().SetValidator(new ContactPersonDto.Validator());
                RuleFor(x => x.BackupContactPerson)
                    .NotEmpty().SetValidator(new ContactPersonDto.Validator()).When(customer => !string.IsNullOrEmpty(customer.BackupContactPerson.Firstname));
            }
        }
    }
}
