using Tests.Stubs;

namespace Tests;

public class VirtualMachine_Should
{
    // Tests thare are no longer relevant after database mapping and scoping of project are commented out. 

    private VirtualMachine? _machine;

    [Fact]
    public void Virtual_machine_cannot_be_assigned_to_host_without_proper_resources()
    {
        Server server = ServerWithoutSpecifications.Create();
        server.Specifications = HostSpecificationsFactory.Create(new List<int> { 1 }, 1, 1);

        _machine = VirtualMachineWithoutSpecifications.Create(server);
        Should.Throw<ArgumentException>(() => _machine.Specifications = new Specifications(1, 1, 2));
    }

    //[Fact]
    //public void Virtual_machine_cannot_increase_specifications_beyond_resources_of_host()
    //{
    //    Server server = ServerWithoutSpecifications.Create();
    //    server.Specifications = HostSpecificationsFactory.Create(new List<int> { 1 }, 1, 1);

    //    _machine = VirtualMachineWithoutSpecifications.Create(server);
    //    _machine.Specifications = new Specifications(1, 1, 1);

    //    Should.Throw<ArgumentException>(() => _machine.Specifications = new Specifications(1, 1, 2));
    //}

    //[Fact]
    //public void Virtual_machine_changing_specifications_updates_remaining_resources_of_host()
    //{
    //    Server server = ServerWithoutSpecifications.Create();
    //    server.Specifications = HostSpecificationsFactory.Create(new List<int> { 1, 2 }, 3, 3);

    //    _machine = VirtualMachineWithoutSpecifications.Create(server);

    //    _machine.Specifications = new Specifications(2, 2, 2);
    //    server.RemainingResources.ShouldBe(new Specifications(1, 1, 1));

    //    _machine.Specifications = new Specifications(3, 2, 1);
    //    server.RemainingResources.ShouldBe(new Specifications(0, 1, 2));
    //}


    [Fact]
    public void Virtual_machine_changing_host_updates_machines_of_old_and_new_host()
    {
        Server s1 = ServerWithoutSpecifications.Create();
        s1.Specifications = HostSpecificationsFactory.Create(new List<int> { 1, 2 }, 3, 3);

        Server s2 = ServerWithoutSpecifications.Create();
        s2.Specifications = HostSpecificationsFactory.Create(new List<int> { 3 }, 3, 3);

        _machine = VirtualMachineWithoutSpecifications.Create(s1);

        _machine.Host = s2;
        s2.Machines.ShouldContain(_machine);
        s1.Machines.ShouldNotContain(_machine);
    }
}
