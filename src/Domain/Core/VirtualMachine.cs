using Domain.Constants;
using Domain.Interfaces;

namespace Domain.Domain;

[ToString]
public class VirtualMachine : Entity, IVirtualMachine
{
    #region Properties
    public string Name { get; set; }
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
    public ISet<Credentials> Credentials { get; set; }
    public Account Account { get; set; }
    public ICustomer Requester { get; set; }
    public ICustomer User { get; set; }
    public Resources Resources { get; set; }
    #endregion

    #region Constructors
    public VirtualMachine(VirtualMachineBuilder builder)
    {
        Name = builder.Name;
        Template = builder.Template;
        Mode = builder.Mode;
        Fqdn = builder.Fqdn;
        Availabilities = builder.Availabilities;
        BackupFrequenty = builder.BackupFrequenty;
        ApplicationDate = builder.ApplicationDate;
        Duration = builder.Duration;
        Status = builder.Status;
        Reason = builder.Reason;
        Ports = builder.Ports;
        Host = builder.Host;
        Credentials = builder.Credentials;
        Account = builder.Account;
        Requester = builder.Requester;
        User = builder.User;
        Resources = builder.Resource;
    }

    #endregion

    #region Builder
    public class VirtualMachineBuilder
    {
        #region Fields
        private string _name;
        private Resources _resource;
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
        private ISet<Credentials> _credentials;
        private Account _account;
        private ICustomer _requester;
        private ICustomer _user;
        #endregion
        #region Properties
        public string Name => _name;
        public Resources Resource => _resource;
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
        public ISet<Credentials> Credentials => _credentials;
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

        public VirtualMachineBuilder SetResource(Resources resource)
        {
            _resource = resource;
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

        public VirtualMachineBuilder SetCredentials(ISet<Credentials> credentials)
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
