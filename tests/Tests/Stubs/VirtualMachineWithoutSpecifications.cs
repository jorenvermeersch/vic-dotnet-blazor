namespace Tests.Stubs;

public static class VirtualMachineWithoutSpecifications
{
    private static int _count = 0;

    private static ContactPerson _contact = new("Jane", "Doe", "jane.doe@hotmail.com", "907-642-6874");
    private static Customer _customer = new InternalCustomer(Institution.HoGent, "DIT", "Toegepaste Informatica", _contact, null, new List<VirtualMachine>());
    private static Account _account = new("Joren", "Vermeersch", "joren.vermeersch@student.hogent.be", Role.Admin, "Fsdt1@t", "DIT", "Toegepaste Informatica");

    public static VirtualMachine Get(Host<VirtualMachine>? server)
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
            Host = server,
            Reason = "reason",
            Ports = new[] { new Port(443, "HTTPS") },
            Credentials = new[] { new Credentials($"user-{_count}", $"P@sW0rd-{_count}!", "user") },
            Account = _account,
            Requester = _customer,
            User = _customer,
            Specifications = new Specifications(0, 0, 0)

        };

        return new VirtualMachine(args);
    }
}
