using Tests.Stubs;

namespace Tests;

public class ServerTest
{
    private Server? _server;

    [Fact]
    public void Server_without_machines_has_all_resources_available_for_use()
    {
        _server = ServerWithoutSpecifications.Get();
        _server.Specifications = new Specifications(2, 2, 2);

        _server.RemainingResources.ShouldBe(new Specifications(2, 2, 2)); // Check with overriden Equals function. 
    }

    [Fact]
    public void Server_with_machines_has_some_remaining_resources()
    {
        _server = ServerWithoutSpecifications.Get();
        _server.Specifications = new Specifications(2, 2, 2);

        VirtualMachine machine = VirtualMachineWithoutSpecifications.Get(_server);
        machine.Specifications = new Specifications(1, 1, 2);

        _server.RemainingResources.ShouldBe(new Specifications(1, 1, 0));
    }

    [Fact]
    public void Server_without_any_unused_processors_cannot_accommodate_additional_machine()
    {

    }

    [Fact]
    public void Server_without_any_unused_memory_cannot_accommodate_additional_machine()
    {

    }

    [Fact]
    public void Server_without_any_unused_storage_cannot_accommodate_additional_machine()
    {

    }
}
