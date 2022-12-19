using Domain.Constants;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using Shared.Customers;

namespace Shared.Validation;

public class CustomerValidator : AbstractValidator<CustomerDto.Mutate>
{
    public CustomerValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.CustomerType)
            .NotNull().WithMessage(ValidationMessages.NotEmpty("Soort"))
            .IsInEnum().WithMessage(ValidationMessages.UnknownEnumValue("Soort", true));

        // Internal customer. 
        RuleFor(x => x.Institution)
           .NotEmpty().When(customer => IsInternal(customer)).WithMessage(ValidationMessages.NotEmpty("Instituut"))
           .IsInEnum().WithMessage(ValidationMessages.UnknownEnumValue("Instituut"));

        RuleFor(x => x.Department)
           .NotEmpty().When(customer => IsInternal(customer)).WithMessage(ValidationMessages.NotEmpty("Departement"));

        RuleFor(x => x.Education)
            .NotEmpty().When(customer => IsInternal(customer)).WithMessage(ValidationMessages.NotEmpty("Opleiding"));

        // External customer. 
        RuleFor(x => x.CompanyName)
            .NotEmpty().When(customer => IsExternal(customer)).WithMessage(ValidationMessages.NotEmpty("Naam"));

        RuleFor(x => x.CompanyType)
            .NotEmpty().When(customer => IsExternal(customer)).WithMessage(ValidationMessages.NotEmpty("Type extern"));

        // Contact information. 
        RuleFor(x => x.ContactPerson)
            .NotEmpty().SetValidator(new ContactPersonValidator());

        RuleFor(x => x.BackupContactPerson)
            .NotEmpty().SetValidator(new ContactPersonValidator()!).When(customer => AnyFieldsFilledIn(customer.BackupContactPerson!));
    }


    private bool IsInternal(CustomerDto.Mutate customer)
    {
        return customer.CustomerType == CustomerType.Intern;
    }

    private bool IsExternal(CustomerDto.Mutate customer)
    {
        return customer.CustomerType == CustomerType.Extern;
    }

    private bool AnyFieldsFilledIn(ContactPersonDto contactPerson)
    {
        string?[] fields = new string?[4] { contactPerson.Firstname, contactPerson.Lastname, contactPerson.Email, contactPerson.Phonenumber };
        return fields.Any(field => !field.IsNullOrEmpty());
    }
}
