﻿using FluentValidation;
using Shared.Host;
using Shared.VirtualMachine;
using static Shared.Host.HostDto;

namespace Client.Validation;

public class CredentialValidation : AbstractValidator<CredentialDto>
{
    private readonly int _nameLength = 2;
    public CredentialValidation()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage(string.Format(FormMessages.NOTEMPTY("Username")))
            .MinimumLength(_nameLength).WithMessage($"Naam heeft minstens {_nameLength} characters");
        RuleFor(x => x.PasswordHash)
            .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").WithMessage(string.Format(FormMessages.INVALIDPASSWORD()))
            .NotEmpty().WithMessage("Geef een waarde in");
        RuleFor(x => x.Role)
           .NotEmpty().WithMessage("Geef een waarde in");
    }
}