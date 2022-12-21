using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Shared.Ports;

public class PortService : IPortService
{
    private readonly VicDBContext dbContext;
    private readonly DbSet<Port> ports;
    public PortService(VicDBContext dbContext)
    {
        this.dbContext = dbContext;
        // _ports = dbContext.Ports;
    }

    public async Task<PortResponse.GetAll> GetAllAsync(PortRequest.GetAll request)
    {
        PortResponse.GetAll response = new();
        var query = ports.AsNoTracking();

        response.Ports = await query.Select(port =>
            new PortDto
            {
                Number = port.Number,
                Service = port.Service,
            }
        ).ToListAsync();

        response.TotalAmount = query.Count();

        return response;
    }

    public async Task<PortResponse.GetDetail> GetDetailAsync(PortRequest.GetDetail request)
    {
        PortResponse.GetDetail response = new();

        response.Port = await ports.AsNoTracking().Where(p => p.Id == request.PortId).Select(p => new PortDto
        {
            Number = p.Number,
            Service = p.Service,
        }).SingleOrDefaultAsync();

        return response;
    }
}

