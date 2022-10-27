using Domain.Interfaces;

namespace Domain.Domain
{
    public abstract class Customer : Entity, ICustomer
    {
        #region Properties
        public ContactPerson ContactPerson { get; set; }
        public ContactPerson BackupContactPerson { get; set; }
        public IList<IVirtualMachine> VirtualMachines { get; set; }
        #endregion

        #region Constructors
        public Customer(ContactPerson contactPerson, ContactPerson backupContact, IList<IVirtualMachine> virtualMachines)
        {
            ContactPerson = contactPerson;
            BackupContactPerson = backupContact;
            VirtualMachines = virtualMachines;
        }
        #endregion
    }
}
