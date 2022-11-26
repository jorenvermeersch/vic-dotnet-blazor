namespace Shared.VirtualMachines;

public class VirtualMachineResponse
{
    public class GetIndex
    {
        public List<VirtualMachineDto.Index> VirtualMachines { get; set; } = new();
        public int TotalAmount { get; set; }
    }

    public class GetDetail
    {
        public VirtualMachineDto.Detail VirtualMachine { get; set; } = default!;
    }

    public class Delete
    {
    }

    public class Create
    {
        public long MachineId { get; set; }
    }

    public class Edit
    {
        public long MachineId { get; set; }
    }
}
