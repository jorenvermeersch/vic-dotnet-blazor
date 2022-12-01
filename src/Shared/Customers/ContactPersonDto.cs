using FluentValidation;
using Shared.Validation;
using Shared.VirtualMachines;

namespace Shared.Customers;
public class ContactPersonDto
{
    public string? Firstname { get; set; } = default!;
    public string? Lastname { get; set; } = default!;
    public string? Email { get; set; } = default!;
    public string? Phonenumber { get; set; }

    public class Validator : AbstractValidator<ContactPersonDto>
    {
        public Validator()
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

}
