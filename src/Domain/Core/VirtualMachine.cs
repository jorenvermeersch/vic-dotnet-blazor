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
    public BackupFrequency BackupFrequency { get; set; }
    public DateTime ApplicationDate { get; set; }
    public TimeSpan TimeSpan { get; set; }
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
        BackupFrequency = builder.BackupFrequency;
        ApplicationDate = builder.ApplicationDate;
        TimeSpan = builder.TimeSpan;
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
        private string _name,
            _fqdn,
            _reason;
        private Specifications _specifications;
        private Template _template;
        private Mode _mode;
        private IList<Availability> _availabilities;
        private BackupFrequency _backupFrequency;
        private DateTime _applicationDate;
        private TimeSpan _timeSpan;
        private Status _status;
        private IList<Port> _ports;
        private Host _host;
        private IList<Credentials> _credentials;
        private Account _account;
        private Customer _requester,
            _user;
        #endregion

        #region Properties
        public string Name => _name;
        public Specifications Specifications => _specifications;
        public Template Template => _template;
        public Mode Mode => _mode;
        public string Fqdn => _fqdn;
        public IList<Availability> Availabilities => _availabilities;
        public BackupFrequency BackupFrequency => _backupFrequency;
        public DateTime ApplicationDate => _applicationDate;
        public TimeSpan TimeSpan => _timeSpan;
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
        public VirtualMachineBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public VirtualMachineBuilder SetSpecifications(Specifications specifications)
        {
            _specifications = specifications;
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

        public VirtualMachineBuilder SetAvailabilities(IList<Availability> availabilities)
        {
            _availabilities = availabilities;
            return this;
        }

        public VirtualMachineBuilder SetBackupBackupFrequency(BackupFrequency backupFrequeny)
        {
            _backupFrequency = backupFrequeny;
            return this;
        }

        public VirtualMachineBuilder SetApplicationDate(DateTime applicationDate)
        {
            _applicationDate = applicationDate;
            return this;
        }

        public VirtualMachineBuilder SetDuration(TimeSpan timeSpan)
        {
            _timeSpan = timeSpan;
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

        public VirtualMachineBuilder SetPorts(IList<Port> ports)
        {
            _ports = ports;
            return this;
        }

        public VirtualMachineBuilder SetHost(Host host)
        {
            _host = host;
            return this;
        }

        public VirtualMachineBuilder SetAccount(Account account)
        {
            _account = account;
            return this;
        }

        public VirtualMachineBuilder SetCredentials(IList<Credentials> credentials)
        {
            _credentials = credentials;
            return this;
        }

        public VirtualMachineBuilder SetRequester(Customer customer)
        {
            _requester = customer;
            return this;
        }

        public VirtualMachineBuilder SetUser(Customer user)
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
