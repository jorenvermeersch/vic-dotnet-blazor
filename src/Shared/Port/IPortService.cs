using System;
using Shared.Port;

namespace Shared.Port
{
	public interface IPortService
	{
        Task<IEnumerable<PortDto>> GetIndexAsync();

    }
}

