using FluentValidation;
using Shared.Hosts;

namespace Shared.Validation;

public class HostSpecificationsValidator : AbstractValidator<HostSpecificationsDto>
{
    public HostSpecificationsValidator()
    {

    }
}
