using FluentValidation;
using Shared.Customers;

namespace Shared.Validation;

public class ContactPersonValidator : AbstractValidator<ContactPersonDto>
{
    public ContactPersonValidator()
    {
        RuleFor(x => x.Firstname).NotEmpty().WithMessage(string.Format(ValidationMessages.NotEmpty("Voornaam")))
            .Matches(ValidationRegex.Name).WithMessage(string.Format(ValidationMessages.INVALID_NAME("Voornaam")));
        RuleFor(x => x.Lastname).NotEmpty().WithMessage(string.Format(ValidationMessages.NotEmpty("Naam")))
            .Matches(ValidationRegex.Name).WithMessage(string.Format(ValidationMessages.INVALID_NAME("Naam")));
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(string.Format(ValidationMessages.NotEmpty("Email")))
            .EmailAddress().WithMessage(string.Format(ValidationMessages.INVALID_EMAIL()));
        RuleFor(x => x.Phonenumber)
            .Matches(ValidationRegex.PhoneNumber);

    }
}
