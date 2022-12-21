using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Shared.Ports;

public class PortService : IPortService
{
    private readonly VicDbContext dbContext;
    private readonly DbSet<Port> ports;
    public PortService(VicDbContext dbContext)
    {
        this.dbContext = dbContext;
        ports = dbContext.Ports;
    }

    public async Task<PortResponse.GetAll> GetAllAsync(PortRequest.GetAll request)
    {
        PortResponse.GetAll response = new();
        var query = ports.AsNoTracking().OrderByDescending(x => x.Number);

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
}

