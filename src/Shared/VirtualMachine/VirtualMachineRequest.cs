using System.ComponentModel;

namespace Shared.VirtualMachine;

public static class VirtualMachineRequest
{
    public class Index
    {
        public string? SearchTerm { get; set; }
        public int Page { get; set; }
        public int Amount { get; set; } = 25;
        public int Offset { get; set; } = 0;
    }

    public class GetByObjectId
    {
        public int ObjectId { get; set; }
        [DefaultValue(0)]
        public int Offset { get; set; }
    }
}
