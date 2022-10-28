using Domain.Interfaces;

namespace Domain.Domain;

[ToString]
public class Server : Entity, IHost
{
    #region Properties
    public string Name { get; set; }
    public Resources Resources { get; set; }

    #endregion

    #region Constructors
    public Server(string name, Resources resources)
    {
        Name = name;
        Resources = resources;
    }
    #endregion
}
