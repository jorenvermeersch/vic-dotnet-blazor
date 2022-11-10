namespace Domain.Core;

[ToString]
public class Server : Host<VirtualMachine>
{
    #region Constructors
    public Server(string name, Specifications resources) : base(name, resources) { }
    #endregion
}
