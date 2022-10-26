using Domain.Controllers;
using Domain.Interfaces;

namespace Domain.Domain;
public class VIC {
    #region Singleton pattern
    private static VIC _instance = new VIC();
    private VIC() { }
    public static VIC Instance => _instance;
    #endregion

    private static VirtualMachineRepo _virtualMachineRepo = new VirtualMachineRepo();
    public IVirtualMachine FindVirtualMachineByName(string name) {
        return _virtualMachineRepo.GetMachine(name);
    }
}
