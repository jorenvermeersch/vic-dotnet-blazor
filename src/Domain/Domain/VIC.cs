using Domain.Repositories;

namespace Domain.Domain;
public class VIC {
    #region Singleton pattern
    private static VIC _instance = new VIC();
    private VIC() {
    VirtualMachineRepo = new VirtualMachineRepo();
    }
    public static VIC Instance => _instance;
    #endregion

    public VirtualMachineRepo VirtualMachineRepo { get; }

    public void CreateVirtualMachine(VirtualMachineArgs args) {
        VirtualMachine vm = new VirtualMachine.VirtualMachineBuilder()
            .
        VirtualMachineRepo.AddMachine(vm);
    }
}
