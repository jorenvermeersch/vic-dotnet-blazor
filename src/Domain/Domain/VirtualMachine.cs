using Domain.Constants;
using Domain.Interfaces;

namespace Domain.Domain;
public class VirtualMachine : IVirtualMachine
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; }
    public int Processors { get; set; }
    public int Memory { get; set; }
    public int Storage { get; set; }
    public Template Template { get; set; }
    public Mode Mode { get; set; }
    public string Fqdn { get; set; }
    public ISet<Availability> Availabilities { get; set; }
    public BackupFrequenty BackupFrequenty { get; set; }
    public DateTime ApplicationDate { get; set; }
    public Duration Duration { get; set; }
    public Status Status { get; set; }
    public string Reason { get; set; }
    public ISet<Port> Ports { get; set; }
    public IHost Host { get; set; }
    public ISet<Credential> Credentials { get; set; }
    public Account Account { get; set; }
    public ICustomer Requester { get; set; }
    public ICustomer User { get; set; }
    #endregion
    #region Constructors
    public VirtualMachine(VirtualMachineBuilder b)
    {
        Name = b.Name;
        Processors = b.Processors;
        Memory = b.Memory;
        Storage = b.Storage;
        Template = b.Template;
        Mode = b.Mode;
        Fqdn = b.Fqdn;
        Availabilities = b.Availabilities;
        BackupFrequenty = b.BackupFrequenty;
        ApplicationDate = b.ApplicationDate;
        Duration = b.Duration;
        Status = b.Status;
        Reason = b.Reason;
        Ports = b.Ports;
        Host = b.Host;
        Credentials = b.Credentials;
        Account = b.Account;
        Requester = b.Requester;
        User = b.User;
    }
    #endregion
    #region Builder
    public class VirtualMachineBuilder
    {
        #region Fields
        private string _name;
        private int _processors;
        private int _memory;
        private int _storage;
        private Template _template;
        private Mode _mode;
        private string _fqdn;
        private ISet<Availability> _availabilities;
        private BackupFrequenty _backupFrequenty;
        private DateTime _applicationDate;
        private Duration _duration;
        private Status _status;
        private string _reason;
        private ISet<Port> _ports;
        private IHost _host;
        private ISet<Credential> _credentials;
        private Account _account;
        private ICustomer _requester;
        private ICustomer _user;
        #endregion
        #region Properties
        public string Name => _name;
        public int Processors => _processors;
        public int Memory => _memory;
        public int Storage => _storage;
        public Template Template => _template;
        public Mode Mode => _mode;
        public string Fqdn => _fqdn;
        public ISet<Availability> Availabilities => _availabilities;
        public BackupFrequenty BackupFrequenty => _backupFrequenty;
        public DateTime ApplicationDate => _applicationDate;
        public Duration Duration => _duration;
        public Status Status => _status;
        public string Reason => _reason;
        public ISet<Port> Ports => _ports;
        public IHost Host => _host;
        public ISet<Credential> Credentials => _credentials;
        public Account Account => _account;
        public ICustomer Requester => _requester;
        public ICustomer User => _user;
        #endregion
        #region Methods
        public VirtualMachineBuilder SetName(string name)
        {
            _name = name;
            return this;
        }
        public VirtualMachineBuilder SetProcessors(int cpu)
        {
            _processors = cpu;
            return this;
        }
        public VirtualMachineBuilder SetMemory(int memory)
        {
            _memory = memory;
            return this;
        }
        public VirtualMachineBuilder SetStorage(int storage)
        {
            _storage = storage;
            return this;
        }
        public VirtualMachineBuilder SetTemplate(Template template)
        {
            _template = template;
            return this;
        }
        public VirtualMachineBuilder SetMode(Mode mode)
        {
            _mode = mode;
            return this;
        }
        public VirtualMachineBuilder SetFqdn(string fqdn)
        {
            _fqdn = fqdn;
            return this;
        }
        public VirtualMachineBuilder SetAvailabilities(ISet<Availability> availabilities)
        {
            _availabilities = availabilities;
            return this;
        }
        public VirtualMachineBuilder SetBackupFrequenty(BackupFrequenty backupFrequenty)
        {
            _backupFrequenty = backupFrequenty;
            return this;
        }
        public VirtualMachineBuilder SetApplicationDate(DateTime applicationDate)
        {
            _applicationDate = applicationDate;
            return this;
        }
        public VirtualMachineBuilder SetDuration(Duration duration)
        {
            _duration = duration;
            return this;
        }
        public VirtualMachineBuilder SetStatus(Status status)
        {
            _status = status;
            return this;
        }
        public VirtualMachineBuilder SetReason(string reason)
        {
            _reason = reason;
            return this;
        }
        public VirtualMachineBuilder SetPorts(ISet<Port> ports)
        {
            _ports = ports;
            return this;
        }
        public VirtualMachineBuilder SetHost(IHost host)
        {
            _host = host;
            return this;
        }
        public VirtualMachineBuilder SetAccount(Account account)
        {
            _account = account;
            return this;
        }
        public VirtualMachineBuilder SetCredentials(ISet<Credential> credentials)
        {
            _credentials = credentials;
            return this;
        }
        public VirtualMachineBuilder SetRequester(ICustomer customer)
        {
            _requester = customer;
            return this;
        }
        public VirtualMachineBuilder SetUser(ICustomer user)
        {
            _user = user;
            return this;
        }
        public VirtualMachine Build()
        {
            return new VirtualMachine(this);
        }
        #endregion
    }
    #endregion
}