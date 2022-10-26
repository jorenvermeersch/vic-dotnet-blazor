namespace Tests;
public class VirtualMachineTests {
    #region Fields
    private Account _account = new("kerem", "yilmaz", Role.Admin, "password", "department", "opleiding");
    private DateTime _appDate = DateTime.Now;
    private HashSet<Availability> _availability = new() { Availability.Monday, Availability.Sunday };
    private BackupFrequenty _bfreq = BackupFrequenty.Monthly;
    private HashSet<Credential> _creds = new() { new("name", "password"), new("user", "password") };
    private Duration _dur = new(DateTime.Now, DateTime.Now.AddDays(20));
    private string _fqdn = "domain.name";
    private IHost _host = new Server("s1", 4, 4, 4);
    private int _memo = 2;
    private Mode _mode = Mode.PAAS;
    private string _name = "VMName";
    private HashSet<Port> _ports = new() { new(1, "port1"), new(2, "port2") };
    private string _reason = "reason";
    private ICustomer _req = new InternalCustomer("educ", "depar", new ContactPerson("fname", "name", "email@valid.com", "0483756789"));
    private Status _status = Status.Requested;
    private int _storage = 2;
    private Template _temp = Template.AI;
    private int _processors = 2;
    #endregion

    [Fact]
    public void VirtualMachine_creation_is_correct() {
        VirtualMachine vm = new VirtualMachine.VirtualMachineBuilder()
            .SetAccount(_account)
            .SetApplicationDate(_appDate)
            .SetAvailabilities(_availability)
            .SetBackupFrequenty(_bfreq)
            .SetCredentials(_creds)
            .SetDuration(_dur)
            .SetFqdn(_fqdn)
            .SetHost(_host)
            .SetMemory(_memo)
            .SetMode(_mode)
            .SetName(_name)
            .SetPorts(_ports)
            .SetReason(_reason)
            .SetRequester(_req)
            .SetStatus(_status)
            .SetStorage(_storage)
            .SetTemplate(_temp)
            .SetUser(_req)
            .SetProcessors(_processors)
            .Build();

        vm.Account.ShouldBe(_account);
        vm.ApplicationDate.ShouldBe(_appDate);
        vm.Availabilities.ShouldBe(_availability);
        vm.BackupFrequenty.ShouldBe(_bfreq);
        vm.Credentials.ShouldBe(_creds);
        vm.Duration.ShouldBe(_dur);
        vm.Fqdn.ShouldBe(_fqdn);
        vm.Host.ShouldBe(_host);
        vm.Memory.ShouldBe(_memo); 
        vm.Mode.ShouldBe(_mode);
        vm.Name.ShouldBe(_name);
        vm.Ports.ShouldBe(_ports);
        vm.Reason.ShouldBe(_reason);
        vm.Requester.ShouldBe(_req);
        vm.Status.ShouldBe(_status);
        vm.Storage.ShouldBe(_storage);
        vm.Template.ShouldBe(_temp);
        vm.User.ShouldBe(_req);
        vm.Processors.ShouldBe(_processors);
    }
}
