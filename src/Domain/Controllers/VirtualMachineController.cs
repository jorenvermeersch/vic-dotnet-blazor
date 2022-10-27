using Domain.Args;
using Domain.Domain;
using Domain.Interfaces;

namespace Domain.Controllers;
public class VirtualMachineController
{
    #region Fields
    private VIC _vic = VIC.Instance;
    #endregion

    #region Methods
    public ISet<IVirtualMachine> GetAll()
    {
        throw new NotImplementedException();
        // TODO: Implement method. 
    }

    public IVirtualMachine GetById(long id)
    {
        throw new NotImplementedException();
        // TODO: Implement method. 
    }
    public ISet<IVirtualMachine> GetByCustomerId(long id)
    {
        throw new NotImplementedException();
        // TODO: Implement method. 
    }

    public void Add(VirtualMachineArgs args)
    {
        _vic.CreateVirtualMachine(args);
    }

    public void UpdateById(long id, VirtualMachineArgs args)
    {
        throw new NotImplementedException();
        // TODO: Implement method. 
    }
    #endregion

}