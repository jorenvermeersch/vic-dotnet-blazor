using Domain.Core;

namespace Domain.Domain;

[ToString]
public class Server : Host
{
    #region Constructors
    public Server(string name, Specifications resources) : base(name, resources) { }
    #endregion
}
