using Domain.Constants;
using Domain.Core;

namespace Domain.Domain;

[ToString]
public class VirtualMachine : Machine
{
    #region Properties
    public Template Template { get; set; }
    public Mode Mode { get; set; }
    public string Fqdn { get; set; }
    public IList<Availability> Availabilities { get; set; } = new List<Availability>();
    public BackupFrequenty BackupFrequenty { get; set; }
    public DateTime ApplicationDate { get; set; }
    public Duration Duration { get; set; }
    public Status Status { get; set; }
    public string Reason { get; set; }
    public IList<Port> Ports { get; set; } = new List<Port>();
    public Host Host { get; set; }
    public IList<Credentials> Credentials { get; set; } = new List<Credentials>();
    public Account Account { get; set; }
    public Customer Requester { get; set; }
    public Customer User { get; set; }
    #endregion

    #region Constructors
    public VirtualMachine(VirtualMachineBuilder builder)
    : base(builder.Name, builder.Specifications)
    {
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
    }

    #endregion

    #region Builder
    public class VirtualMachineBuilder
    {
        #region Fields
        private string _name, _fqdn, _reason;
        private Specifications _specifications;
        private Template _template;
        private Mode _mode;
        private IList<Availability> _availabilities;
        private BackupFrequenty _backupFrequenty;
        private DateTime _applicationDate;
        private Duration _duration;
        private Status _status;
        private IList<Port> _ports;
        private Host _host;
        private IList<Credentials> _credentials;
        private Account _account;
        private Customer _requester, _user;

        #endregion

        #region Properties
        public string Name => _name;
        public Specifications Specifications => _specifications;
        public Template Template => _template;
        public Mode Mode => _mode;
        public string Fqdn => _fqdn;
        public IList<Availability> Availabilities => _availabilities;
        public BackupFrequenty BackupFrequenty => _backupFrequenty;
        public DateTime ApplicationDate => _applicationDate;
        public Duration Duration => _duration;
        public Status Status => _status;
        public string Reason => _reason;
        public IList<Port> Ports => _ports;
        public Host Host => _host;
        public IList<Credentials> Credentials => _credentials;
        public Account Account => _account;
        public Customer Requester => _requester;
        public Customer User => _user;
        #endregion

        #region Methods
        public VirtualMachineBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public VirtualMachineBuilder WithSpecifications(Specifications specifications)
        {
            _specifications = specifications;
            return this;
        }

        public VirtualMachineBuilder WithTemplate(Template template)
        {
            _template = template;
            return this;
        }

        public VirtualMachineBuilder WithMode(Mode mode)
        {
            _mode = mode;
            return this;
        }

        public VirtualMachineBuilder WithFqdn(string fqdn)
        {
            _fqdn = fqdn;
            return this;
        }

        public VirtualMachineBuilder WithAvailabilities(IList<Availability> availabilities)
        {
            _availabilities = availabilities;
            return this;
        }

        public VirtualMachineBuilder WithBackupFrequenty(BackupFrequenty backupFrequenty)
        {
            _backupFrequenty = backupFrequenty;
            return this;
        }

        public VirtualMachineBuilder WithApplicationDate(DateTime applicationDate)
        {
            _applicationDate = applicationDate;
            return this;
        }

        public VirtualMachineBuilder WithDuration(Duration duration)
        {
            _duration = duration;
            return this;
        }

        public VirtualMachineBuilder WithStatus(Status status)
        {
            _status = status;
            return this;
        }

        public VirtualMachineBuilder WithReason(string reason)
        {
            _reason = reason;
            return this;
        }

        public VirtualMachineBuilder WithPorts(IList<Port> ports)
        {
            _ports = ports;
            return this;
        }

        public VirtualMachineBuilder WithHost(Host host)
        {
            _host = host;
            return this;
        }

        public VirtualMachineBuilder WithAccount(Account account)
        {
            _account = account;
            return this;
        }

        public VirtualMachineBuilder WithCredentials(IList<Credentials> credentials)
        {
            _credentials = credentials;
            return this;
        }

        public VirtualMachineBuilder WithRequester(Customer customer)
        {
            _requester = customer;
            return this;
        }

        public VirtualMachineBuilder WithUser(Customer user)
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
