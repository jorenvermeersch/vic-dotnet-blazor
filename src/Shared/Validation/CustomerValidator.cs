using FluentValidation;
using Shared.Customers;

namespace Shared.Validation;

public class CustomerValidator : AbstractValidator<CustomerDto.Mutate>
{
    public CustomerValidator()
    {
        RuleFor(x => x.CustomerType)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("Soort"));

        RuleFor(x => x.CompanyName)
            .NotEmpty().When(customer => customer.CustomerType == "Intern").WithMessage(ValidationMessages.NotEmpty("Naam"));
        RuleFor(x => x.CompanyType)
            .NotEmpty().When(customer => customer.CustomerType == "Intern").WithMessage(ValidationMessages.NotEmpty("Type extern"));
        RuleFor(x => x.Department)
            .NotEmpty().When(customer => customer.CustomerType == "Intern").WithMessage(ValidationMessages.NotEmpty("Departement"));
        RuleFor(x => x.Institution)
            .NotEmpty().When(customer => customer.CustomerType == "Intern").WithMessage(ValidationMessages.NotEmpty("Instituut"));

        RuleFor(x => x.ContactPerson)
            .NotEmpty().SetValidator(new ContactPersonValidator());
        RuleFor(x => x.BackupContactPerson)
            .NotEmpty().SetValidator(new ContactPersonValidator()).When(customer => !string.IsNullOrEmpty(customer.BackupContactPerson.Firstname));
    }


}
