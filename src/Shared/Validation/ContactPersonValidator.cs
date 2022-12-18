using FluentValidation;
using Shared.Customers;

namespace Shared.Validation;

public class ContactPersonValidator : AbstractValidator<ContactPersonDto>
{
    public ContactPersonValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Firstname).NotEmpty().WithMessage(ValidationMessages.NotEmpty("Voornaam"))
            .Matches(ValidationRegex.Name).WithMessage(ValidationMessages.InvalidName("Voornaam"));

        RuleFor(x => x.Lastname).NotEmpty().WithMessage(ValidationMessages.NotEmpty("Naam"))
            .Matches(ValidationRegex.Name).WithMessage(ValidationMessages.InvalidName("Naam"));

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidationMessages.NotEmpty("E-mailadres"))
            .EmailAddress().WithMessage(ValidationMessages.InvalidEmailAddress);

        RuleFor(x => x.Phonenumber)
            .Matches(ValidationRegex.PhoneNumber).WithMessage(ValidationMessages.InvalidPhoneNumber);

    }
}
