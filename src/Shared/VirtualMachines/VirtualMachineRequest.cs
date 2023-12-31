﻿namespace Shared.VirtualMachines;

public static class VirtualMachineRequest
{
    public class GetIndex
    {
        public string? SearchTerm { get; set; }
        public int Page { get; set; }
        public int Amount { get; set; } = 25;
        public int Offset { get; set; } = 0;
        public bool IsUnfinished { get; set; } = false;
    }

    public class GetDetail
    {
        public long MachineId { get; set; }
    }

    public class GetAllDetails
    {

    }

    public class Delete
    {
        public long MachineId { get; set; }
    }

    public class Create
    {
        public VirtualMachineDto.Mutate VirtualMachine { get; set; } = default!;
    }

    public class Edit
    {
        public long MachineId { get; set; }
        public VirtualMachineDto.Mutate VirtualMachine { get; set; } = default!;
    }
}
