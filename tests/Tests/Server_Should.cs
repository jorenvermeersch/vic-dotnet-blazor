using Tests.Stubs;

namespace Tests;

public class Server_Should
{
    private Server? _server;

    [Fact]
    public void Server_without_machines_has_all_resources_available_for_use()
    {
        _server = ServerWithoutSpecifications.Create();
        _server.Specifications = HostSpecificationsFactory.Create(new List<int> { 2 }, 2, 2);

        _server.RemainingResources.ShouldBe(new Specifications(2, 2, 2)); // Check with overriden Equals function. 
    }

    [Fact]
    public void Server_updates_remaining_resources_after_adding_new_machine()
    {
        _server = ServerWithoutSpecifications.Create();
        _server.Specifications = HostSpecificationsFactory.Create(new List<int> { 4, 2, 1, 1 }, 8, 8);

        VirtualMachine m1 = VirtualMachineWithoutSpecifications.Create(_server);
        m1.Specifications = new Specifications(1, 1, 2);

        VirtualMachine m2 = VirtualMachineWithoutSpecifications.Create(_server);
        m2.Specifications = new Specifications(3, 1, 3);

        _server.RemainingResources.Equals(new Specifications(4, 6, 3));
        new List<VirtualMachine> { m1, m2 }.ForEach(machine => _server.Machines.ShouldContain(machine));
    }

    [Fact]
    public void Server_cannot_decrease_specifications_to_lower_than_required_for_assigned_machines()
    {
        _server = ServerWithoutSpecifications.Create();
        _server.Specifications = HostSpecificationsFactory.Create(new List<int> { 1, 1 }, 8, 8);

        VirtualMachine machine = VirtualMachineWithoutSpecifications.Create(_server);
        machine.Specifications = new Specifications(1, 1, 2);

        Should.Throw<ArgumentException>(() => _server.Specifications = HostSpecificationsFactory.Create(new List<int> { 1 }, 1, 1));
    }

    [Fact]
    public void Server_without_any_unused_processors_cannot_accommodate_additional_machine()
    {
        _server = ServerWithoutSpecifications.Create();
        _server.Specifications = HostSpecificationsFactory.Create(new List<int> { }, 2, 2);

        VirtualMachine machine = VirtualMachineWithoutSpecifications.Create(_server);
        Should.Throw<ArgumentException>(() => machine.Specifications = new Specifications(1, 1, 1));
    }

    [Fact]
    public void Server_without_any_unused_memory_cannot_accommodate_additional_machine()
    {
        _server = ServerWithoutSpecifications.Create();
        _server.Specifications = HostSpecificationsFactory.Create(new List<int> { 2 }, 0, 2);

        VirtualMachine machine = VirtualMachineWithoutSpecifications.Create(_server);
        Should.Throw<ArgumentException>(() => machine.Specifications = new Specifications(1, 1, 1));
    }

    [Fact]
    public void Server_without_any_unused_storage_cannot_accommodate_additional_machine()
    {
        _server = ServerWithoutSpecifications.Create();
        _server.Specifications = HostSpecificationsFactory.Create(new List<int> { 2 }, 2, 0);

        VirtualMachine machine = VirtualMachineWithoutSpecifications.Create(_server);
        Should.Throw<ArgumentException>(() => machine.Specifications = new Specifications(1, 1, 1));
    }

    [Fact]
    public void Server_virtual_processors_are_equal_to_sum_of_core_count_multiplied_by_virtualisation_factor()
    {
        List<KeyValuePair<Processor, int>> processors = new();

        // Key : Value => Cores : VirtualisationFactor.
        Dictionary<int, int> values = new() { { 1, 1 }, { 4, 2 }, { 16, 2 }, { 2, 1 } };

        foreach (var entry in values)
        {
            Processor processor = HostSpecificationsFactory.CreateProcessor(entry.Key, 1);
            processors.Add(new KeyValuePair<Processor, int>(processor, entry.Value));
        }

        _server = ServerWithoutSpecifications.Create();
        _server.Specifications = new HostSpecifications(processors, 1, 1);

        _server.Specifications.Processors.ShouldBe(43);
    }

    [Fact]
    public void Adding_processor_to_server_updates_specifications_and_remaining_resources()
    {
        _server = ServerWithoutSpecifications.Create();
        _server.Specifications = HostSpecificationsFactory.Create(new List<int> { 2 }, 2, 2);

        Processor processor = HostSpecificationsFactory.CreateProcessor(2, 2);
        _server.AddProcessor(processor, 2);

        _server.Specifications.Equals(new Specifications(6, 2, 2));
        _server.RemainingResources.Equals(new Specifications(6, 2, 2));
    }

    [Fact]
    public void Removing_processor_to_server_updates_specifications_and_remaining_resources()
    {
        _server = ServerWithoutSpecifications.Create();
        _server.Specifications = HostSpecificationsFactory.Create(new List<int> { 2 }, 2, 2);

        int virtualisationFactor = 2;

        Processor processor = HostSpecificationsFactory.CreateProcessor(2, 2);
        _server.AddProcessor(processor, virtualisationFactor);
        _server.Specifications.Equals(new Specifications(6, 2, 2));

        _server.RemoveProcessor(processor, virtualisationFactor);
        _server.Specifications.Equals(new Specifications(2, 2, 2));
    }

    [Fact]
    public void Server_cannot_remove_processor_if_new_specifications_cannot_accommodate_assigned_machines()
    {
        _server = ServerWithoutSpecifications.Create();
        _server.Specifications = HostSpecificationsFactory.Create(new List<int> { }, 2, 2);
        Processor processor = HostSpecificationsFactory.CreateProcessor(2, 2);

        int virtualisationFactor = 2;
        _server.AddProcessor(processor, virtualisationFactor);

        VirtualMachine machine = VirtualMachineWithoutSpecifications.Create(_server);
        machine.Specifications = new(2, 2, 2);

        Should.Throw<ArgumentException>(() => _server.RemoveProcessor(processor, virtualisationFactor));
    }

    [Fact]
    public void Server_has_history_after_being_created()
    {
        _server = ServerWithoutSpecifications.Create();
        _server.History.Count.ShouldBe(1);
    }

    [Fact]
    public void Server_adds_history_after_updating_specifications()
    {
        _server = ServerWithoutSpecifications.Create();
        _server.Specifications = HostSpecificationsFactory.Create(new List<int> { }, 2, 2);
        _server.History.Count.ShouldBe(2);
    }

    [Fact]
    public void Server_adds_history_after_adding_processor()
    {
        _server = ServerWithoutSpecifications.Create();
        Processor processor = HostSpecificationsFactory.CreateProcessor(2, 2);
        _server.AddProcessor(processor, 1);
        _server.History.Count.ShouldBe(2);

    }

    [Fact]
    public void Server_adds_history_after_removing_processor()
    {
        _server = ServerWithoutSpecifications.Create();
        Processor processor = HostSpecificationsFactory.CreateProcessor(2, 2);
        int virtualisatieFactor = 1;
        _server.AddProcessor(processor, virtualisatieFactor);
        _server.RemoveProcessor(processor, virtualisatieFactor);

        _server.History.Count.ShouldBe(3);
    }

    [Fact]
    public void Server_adds_history_after_adding_virtual_machine()
    {
        _server = ServerWithoutSpecifications.Create();
        _server.Specifications = HostSpecificationsFactory.Create(new List<int> { 2 }, 2, 2);

        VirtualMachineWithoutSpecifications.Create(_server);
        _server.History.Count.ShouldBe(3);
    }

    [Fact]
    public void Server_adds_history_after_removing_virtual_machine()
    {
        _server = ServerWithoutSpecifications.Create();
        _server.Specifications = HostSpecificationsFactory.Create(new List<int> { 2 }, 2, 2);

        VirtualMachine machine = VirtualMachineWithoutSpecifications.Create(_server);
        _server.RemoveMachine(machine);

        _server.History.Count.ShouldBe(4);
    }

    [Fact]
    public void Server_adds_history_after_changing_running_virtual_machine_specifications()
    {
        _server = ServerWithoutSpecifications.Create();
        _server.Specifications = HostSpecificationsFactory.Create(new List<int> { 2 }, 2, 2);

        VirtualMachine machine = VirtualMachineWithoutSpecifications.Create(_server);
        machine.Specifications = new(2, 2, 2);

        _server.History.Count.ShouldBe(4);
    }
}
