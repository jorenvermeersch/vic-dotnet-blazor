namespace Tests.Stubs;

public class ServerWithoutSpecifications
{
    private static int count = 0;
    public static Server Get()
    {
        count++;
        return new Server($"test-server-{count}", new Specifications(0, 0, 0), new HashSet<VirtualMachine>());
    }
}
