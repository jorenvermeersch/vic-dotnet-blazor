using FluentValidation;
using Shared.Customers;

namespace Shared.Validation;

public class ContactPersonValidator : AbstractValidator<ContactPersonDto>
{
    public ContactPersonValidator()
    {
        RuleFor(x => x.Firstname).NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Voornaam")))
            .Matches(ValidationRegex.Name).WithMessage(string.Format(FormMessages.INVALIDNAME("Voornaam")));
        RuleFor(x => x.Lastname).NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Naam")))
            .Matches(ValidationRegex.Name).WithMessage(string.Format(FormMessages.INVALIDNAME("Naam")));
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Email")))
            .EmailAddress().WithMessage(string.Format(FormMessages.INVALIDEMAIL()));
        RuleFor(x => x.Phonenumber)
            .Matches(ValidationRegex.PhoneNumber);

    }
}
