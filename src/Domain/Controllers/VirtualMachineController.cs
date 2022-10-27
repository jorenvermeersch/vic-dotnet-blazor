using Domain.Args;
using Domain.Domain;

namespace Domain.Controllers;
public class VirtualMachineController {
    private VIC _vic = VIC.Instance;

    public ISet<VirtualMachine> FindAllVirtualMachines() {
        return _vic.VirtualMachineRepo.Machines;
    }
    public VirtualMachine FindVirtualMachineByFqdn(string fqdn) {
        return _vic.VirtualMachineRepo.GetMachineByFqdn(fqdn);
    }
    public ISet<VirtualMachine> FindVirtualMachinesByUserEmail(string email) {
        return _vic.VirtualMachineRepo.GetVirtualMachinesByUserEmail(email);
    }

    public void AddVirtualMachine(VirtualMachineArgs args)
    {
        _vic.CreateVirtualMachine(args);
    }

    public VirtualMachineController() {}
}