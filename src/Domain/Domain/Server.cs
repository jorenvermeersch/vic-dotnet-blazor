using Domain.Interfaces;

namespace Domain.Domain;
public class Server : IHost
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; }
    public Resource Resource { get; set; }
    #endregion
    #region Constructors
    public Server(string name, Resource resource)
    {
        Name = name;
        Resource = resource;
    }
    #endregion
}
