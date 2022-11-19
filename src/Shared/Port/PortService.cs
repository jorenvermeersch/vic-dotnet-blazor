using System;
using Bogus;
using Shared.Specification;
using Shared.VirtualMachine;

namespace Shared.Port
{
	public class PortService:IPortService
	{
        public readonly List<PortDto> ports = new();

        public PortService()
		{

            ports.Add(new PortDto()
            {
                Id = 1,
                Service = "HTTPS",
                Number = 443
            });
            ports.Add(new PortDto()
            {
                Id = 2,
                Service = "HTTP",
                Number = 80
            });
            ports.Add(new PortDto()
            {
                Id = 1,
                Service = "SSH",
                Number = 22
            });
        }

        public Task<IEnumerable<PortDto>> GetIndexAsync()
        {
            return Task.FromResult(ports.Select(x=>x));
        }
    }
}

