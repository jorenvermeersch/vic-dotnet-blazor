using Domain.Constants;
using FluentValidation;
using Shared.Validation;

namespace Shared.Accounts;

public static class AccountDto
{
    public class Index
    {
        public long Id { get; set; }
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public Role Role { get; set; }
        public bool IsActive { get; set; }
    }

    public class Detail : Index
    {
        public string Department { get; set; } = default!;
        public string Education { get; set; } = default!;
    }
    public class Mutate
    {
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Role { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        public string Password { get; set; } = default!;
        public string Department { get; set; } = default!;
        public string Education { get; set; } = default!;

        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.Firstname)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Voornaam")))
            .Matches(ValidationRegex.Name).WithMessage(string.Format(FormMessages.INVALIDNAME("Voornaam")))
            .MinimumLength(2).WithMessage($"Voornaam heeft minstens 2 characters");
                RuleFor(x => x.Lastname)
                    .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Naam")))
                    .Matches(ValidationRegex.Name).WithMessage(string.Format(FormMessages.INVALIDNAME("Voornaam")))
                    .MinimumLength(2).WithMessage($"Naam heeft minstens 2 characters");

                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Email")))
                    .EmailAddress().WithMessage(string.Format(FormMessages.INVALIDEMAIL()));
                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Wachtwoord")))
                    .Matches(ValidationRegex.Password).WithMessage(string.Format(FormMessages.INVALIDPASSWORD()));
                RuleFor(x => x.Role)
                    .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Rol")));
                RuleFor(x => x.Department)
                    .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Departement")));
            }
        }
    }
}