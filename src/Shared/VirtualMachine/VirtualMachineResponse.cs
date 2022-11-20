using Shared.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.VirtualMachine
{
    public class VirtualMachineResponse
    {
        public class GetIndex
        {
            public List<VirtualMachineDto.Index> VirtualMachines { get; set; } = new();
            public int TotalAmount { get; set; }
        }

        public class GetDetail
        {

        }

        public class Delete
        {
        }

        public class Create
        {

        }

        public class Edit
        {

        }
    }
}
