using Shared.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.VirtualMachine;

public static class VirtualMachineRequest
{
    public class GetIndex
    {
        public string SearchTerm { get; set; } = string.Empty;
        public int Offset { get; set; }
    }

    public class GetByObjectId
    {
        public int ObjectId { get; set; }
        [DefaultValue(0)]
        public int Offset { get; set; }
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
