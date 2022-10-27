using Domain.Interfaces;

namespace Domain.Domain;
[ToString]
public class Server : Entity, IHost
{
    #region Properties
    public string Name { get; set; }
    public Resources Resource { get; set; }

    #endregion
    #region Constructors
    public Server(string name, Resources resource)
    {
        Name = name;
        Resource = resource;
    }
    #endregion
}
