using FluentValidation;
using Shared.Account;
using Shared.customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Validation;

public class ContactPersonValidator : AbstractValidator<ContactPersonDto>
{
	public ContactPersonValidator()
	{
		RuleFor(x => x.Firstname).NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Voornaam")))
			.Matches(Validation.Name).WithMessage(string.Format(FormMessages.INVALIDNAME("Voornaam")));
        RuleFor(x => x.Lastname).NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Naam")))
            .Matches(Validation.Name).WithMessage(string.Format(FormMessages.INVALIDNAME("Naam")));
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Email")))
            .EmailAddress().WithMessage(string.Format(FormMessages.INVALIDEMAIL()));
        RuleFor(x => x.Phonenumber)
            .Matches(Validation.PhoneNumber);

    }
}
