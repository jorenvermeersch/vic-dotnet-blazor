using Domain.Hosts;
using Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Shared.Ports;

public class PortService : IPortService
{
    private readonly VicDBContext _dbContext;
    private readonly DbSet<Port> _ports;
    public PortService(VicDBContext dbContext)
    {
        _dbContext= dbContext;
        _ports = dbContext.Ports;
    }

    public async Task<PortResponse.GetAll> GetAllAsync(PortRequest.GetAll request)
    {
        PortResponse.GetAll response = new();
        var query = _ports.AsNoTracking();

        response.Ports = await query.Select(p => new PortDto
        {
            Number = p.Number,
            Service = p.Service,
        }).ToListAsync();

        response.TotalAmount = query.Count();

        return response;
    }

    public async Task<PortResponse.GetDetail> GetDetailAsync(PortRequest.GetDetail request)
    {
        PortResponse.GetDetail response = new();

        response.Port = await _ports .AsNoTracking().Where(p => p.Id == request.PortId).Select(p=>new PortDto
        {
            Number = p.Number,
            Service = p.Service,
        }).SingleOrDefaultAsync();

        return response;
    }
}

