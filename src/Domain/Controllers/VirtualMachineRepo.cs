using Domain.Constants;
using Domain.Domain;

namespace Domain.Controllers;
public class VirtualMachineRepo : IMachineRepository<VirtualMachine> {
	private readonly ISet<VirtualMachine> _machines = new HashSet<VirtualMachine>();
	public ISet<VirtualMachine> Machines => _machines;
	public VirtualMachineRepo() {;
		SeedData();
	}
	public void AddMachine(VirtualMachine machine) {
		Machines.Add(machine);
	}

	public VirtualMachine GetMachine(string name) {
		return Machines.First(m => m.Name == name);
	}
	private void SeedData() {
		VirtualMachine vm1 = new VirtualMachine.VirtualMachineBuilder()
			.SetAccount(new Account("kerem", "yilmaz", Role.Admin, "password", "DIT", "Toegepaste Informatica"))
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
            .SetAccount(new Account("angela", "degryse", Role.Observer, "password", "DIT", "Toegepaste Informatica"))
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