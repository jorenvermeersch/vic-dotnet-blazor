using Domain.Constants;
using Domain.Domain;

namespace Domain.Controllers;
public class VirtualMachineController {
    private VIC _vic = VIC.Instance;

    public ISet<VirtualMachine> GetVirtualMachines() {
        return _vic.VirtualMachineRepo.Machines;
    }

    public ISet<VirtualMachine> GetVirtualMachinesFromUser(string name) {
        return _vic.VirtualMachineRepo.GetVirtualMachineFromUser(name);
    }

    public void AddVirtualMachine(VirtualMachineArgs args)
    {
        _vic.CreateVirtualMachine(args);
    }

    public VirtualMachineController() {}
}