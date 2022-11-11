namespace Tests.Stubs;

public static class VirtualMachineWithoutSpecifications
{
    private static int _count = 0;

    private static Customer _customer = new InternalCustomer(Institution.HoGent, "DIT", "Toegepaste Informatica", null, null, new List<VirtualMachine>());

    // TODO: Finish. 

    public static VirtualMachine Get(Server server)
    {
        _count++;
        VirtualMachineArgs args = new()
        {
            Name = $"virtual-machine-{_count}",
            Template = Template.NoTemplate,
            Mode = Mode.IAAS,
            Fqdn = $"devops-hogent-{_count}",
            Availabilities = new[] { Availability.Wednesday },
            ApplicationDate = DateTime.Now,
            TimeSpan = new Domain.Core.TimeSpan(DateTime.Now, DateTime.Now),
            Status = Status.Deployed,
            Host = null,
            Reason = "reason",
            Ports = new[] { new Port(443, "HTTPS") },
            Credentials = new[] { new Credentials($"user-{_count}", $"P@sW0rd-{_count}!", "user") },
            Account = null,
            Requester = _customer,
            User = _customer,
            Specifications = new Specifications(0, 0, 0)

        };

        return new VirtualMachine(args);
    }
}
