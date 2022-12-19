using Domain.VirtualMachines;
using Shared.Ports;

namespace Services.Ports;

public class FakePortService : IPortService
{
    //private static readonly List<Port> Ports = new();

    public static List<Port> Ports { get;private set; } = new List<Port>();

    static FakePortService()
    {
        Ports.AddRange(new List<Port> { new Port(443, "HTTPS") { Id = 1 }, new Port(80, "HTTP") { Id = 2 }, new Port(22, "SSH") { Id = 3 } });
    }

    public async Task<PortResponse.GetAll> GetAllAsync(PortRequest.GetAll request)
    {
        // FILTEREN OP NUMMER

        var query = Ports.AsQueryable();

        int totalAmount = query.Count();

        var items = query
           .Skip((request.Page - 1) * request.Amount)
           .Take(request.Amount)
           .OrderBy(x => x.Id)
           .Select(x => new PortDto
           {
               Number = x.Number,
               Service =x.Service
           }).ToList();

        var result = new PortResponse.GetAll
        {
            Ports = items,
            TotalAmount = totalAmount
        };

        return result;
    }

    public async Task<PortResponse.GetDetail> GetDetailAsync(PortRequest.GetDetail request)
    {
        PortResponse.GetDetail response = new();

        response.Port = Ports.Where(x => x.Id == request.PortId).Select(x => new PortDto
        {
            Number = x.Number,
            Service = x.Service
        }).SingleOrDefault();

        return response;
    }
}
