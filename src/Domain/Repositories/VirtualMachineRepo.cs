using Domain.Constants;
using Domain.Domain;
using Domain.Interfaces;

namespace Domain.Repositories;
public class VirtualMachineRepo : IMachineRepository<VirtualMachine>
{
    #region Fields
    private readonly ISet<VirtualMachine> _machines = new HashSet<VirtualMachine>();
    public ISet<VirtualMachine> Machines => _machines;
    #endregion
    #region Constructors
    public VirtualMachineRepo() {
        SeedData();
    }
    #endregion
    #region Methods
    public void AddMachine(VirtualMachine vm) {
        Machines.Add(vm);
    }
    public VirtualMachine GetMachineByFqdn(string fqdn) {
        return _machines.First(m => m.Fqdn == fqdn);
    }
    public ISet<VirtualMachine> GetVirtualMachinesByUserEmail(string email) {
        return _machines.Where(m => m.User?.ContactPerson.Email == email).ToHashSet();
    }
    private void SeedData() {
        VirtualMachine vm1 = new VirtualMachine.VirtualMachineBuilder()
            .SetAccount(new Account("kerem", "yilmaz","kerem.yilmaz@hotmail.com", Role.Admin, "password", "DIT", "Toegepaste Informatica"))
            .SetApplicationDate(DateTime.Now)
            .SetAvailabilities(new HashSet<Availability> { Availability.Saturday, Availability.Monday })
            .SetBackupFrequenty(BackupFrequenty.Daily)
            .SetCredentials(new HashSet<Credential>() { new("Admin", "password"), new("User", "password") })
            .SetDuration(new Duration(DateTime.Now, DateTime.Now.AddMonths(1)))
            .SetFqdn("com.vic.vm1")
            .SetHost(new Server("server1", new Resource(4, 4, 4)))
            .SetMode(Mode.IAAS)
            .SetPorts(new HashSet<Port>() { new(1, "port1"), new(2, "port2") })
            .SetReason("Ik heb een VM nodig voor Ai te trainen.")
            .SetName("VirtualMachine1")
            .SetRequester(new InternalCustomer("Toegepaste Informatica", "DIT", new ContactPerson("Kerem", "Yilmaz", "kerem.yilmaz@valid.com", "0483447325")))
            .SetResource(new Resource(2, 2, 2))
            .SetStatus(Status.Requested)
            .SetTemplate(Template.AI)
            .SetUser(new InternalCustomer("Toegepaste Informatica", "DIT", new ContactPerson("Kerem", "Yilmaz", "kerem.yilmaz@valid.com", "0483788945")))
            .Build();

        VirtualMachine vm2 = new VirtualMachine.VirtualMachineBuilder()
            .SetAccount(new Account("angela", "degryse","angela.degryse@hotmail.com", Role.Observer, "password", "DIT", "Toegepaste Informatica"))
            .SetApplicationDate(DateTime.Now)
            .SetAvailabilities(new HashSet<Availability> { Availability.Monday, Availability.Tuesday, Availability.Wednesday })
            .SetBackupFrequenty(BackupFrequenty.Monthly)
            .SetCredentials(new HashSet<Credential>() { new("Admin", "password"), new("User", "password"), new("User2", "password") })
            .SetDuration(new Duration(DateTime.Now, DateTime.Now.AddMonths(2)))
            .SetFqdn("com.vic.vm2")
            .SetHost(new Server("server2", new Resource(4, 4, 4)))
            .SetMode(Mode.PAAS)
            .SetPorts(new HashSet<Port>() { new(1, "port1"), new(2, "port2") })
            .SetReason("Ik heb een VM nodig om mijn website te hosten.")
            .SetName("VirtualMachine2")
            .SetRequester(new InternalCustomer("Toegepaste Informatica", "DIT", new ContactPerson("Angela", "Degryse", "angela.degryse@valid.com", "0483567812")))
            .SetResource(new Resource(2, 2, 2))
            .SetStatus(Status.Requested)
            .SetTemplate(Template.AI)
            .SetUser(new InternalCustomer("Toegepaste Informatica", "DIT", new ContactPerson("Angela", "Degryse", "angela.degryse@valid.com", "0483567812")))
            .Build();

        AddMachine(vm1);
        AddMachine(vm2);
    }
}
#endregion
