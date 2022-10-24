namespace Domain;

public class ExternalCustomer : ICustomer
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public ContactPerson ContactPerson { get; set; }
    public ContactPerson BackupContactPerson { get; set; }
    public IList<IVirtualMachine> VirtualMachines { get; set; }
    #endregion
    #region Construcors
    public ExternalCustomer(string name, string type, ContactPerson contactPerson, ContactPerson? backupContact=null) {
        throw new NotImplementedException();
    }
    #endregion

}
