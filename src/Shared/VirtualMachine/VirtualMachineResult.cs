namespace Shared.VirtualMachine;

public class VirtualMachineResult
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
