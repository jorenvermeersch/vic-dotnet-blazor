using Ardalis.GuardClauses;
using Domain.Constants;
using System.Text.RegularExpressions;

namespace Domain.Core;

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
    public Duration TimeSpan { get; set; }
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
        Template = Guard.Against.EnumOutOfRange(builder.Template, nameof(Template));
        Mode = Guard.Against.EnumOutOfRange(builder.Mode, nameof(Mode));
        Fqdn = Guard.Against.NullOrInvalidInput(
            builder.Fqdn,
            nameof(Fqdn),
            input => Regex.IsMatch(input ?? "", Validation.Fqdn)
        );
        Availabilities = Guard.Against.NullOrInvalidInput(
            builder.Availabilities,
            nameof(Availabilities),
            input => input.Count > 0
        );
        BackupFrequency = Guard.Against.EnumOutOfRange(BackupFrequency, nameof(BackupFrequency));
        ApplicationDate = Guard.Against.Null(builder.ApplicationDate, nameof(ApplicationDate)); // Can be earlier than today.
        TimeSpan = Guard.Against.Null(builder.TimeSpan, nameof(TimeSpan));
        Status = Guard.Against.EnumOutOfRange(builder.Status, nameof(Status));
        Reason = Guard.Against.NullOrWhiteSpace(builder.Reason, nameof(Reason));
        Ports = Guard.Against.NullOrInvalidInput(
            builder.Ports,
            nameof(Ports),
            input => input.Count > 0
        );
        Host = Guard.Against.Null(builder.Host, nameof(Host));
        Credentials = Guard.Against.Null(builder.Credentials, nameof(Credentials));
        Account = Guard.Against.Null(builder.Account, nameof(Account));
        Requester = Guard.Against.Null(builder.Requester, nameof(Requester));
        User = Guard.Against.Null(builder.User, nameof(User));
    }

    #endregion

    #region Builder
    public class VirtualMachineBuilder
    {
        #region Properties
        public string? Name { get; private set; }
        public Specifications? Specifications { get; private set; }
        public Template Template { get; private set; }
        public Mode Mode { get; private set; }
        public string? Fqdn { get; private set; }
        public IList<Availability> Availabilities { get; private set; } = new List<Availability>();
        public BackupFrequency BackupFrequency { get; private set; }
        public DateTime ApplicationDate { get; private set; }
        public Duration? TimeSpan { get; private set; }
        public Status Status { get; private set; }
        public string? Reason { get; private set; }
        public IList<Port> Ports { get; private set; } = new List<Port>();
        public Host? Host { get; private set; }
        public IList<Credentials> Credentials { get; private set; } = new List<Credentials>();
        public Account? Account { get; private set; }
        public Customer? Requester { get; private set; }
        public Customer? User { get; private set; }
        #endregion

        #region Methods
        public VirtualMachineBuilder SetName(string name)
        {
            Name = name;
            return this;
        }

        public VirtualMachineBuilder SetSpecifications(Specifications specifications)
        {
            Specifications = specifications;
            return this;
        }

        public VirtualMachineBuilder SetTemplate(Template template)
        {
            Template = template;
            return this;
        }

        public VirtualMachineBuilder SetMode(Mode mode)
        {
            Mode = mode;
            return this;
        }

        public VirtualMachineBuilder SetFqdn(string fqdn)
        {
            Fqdn = fqdn;
            return this;
        }

        public VirtualMachineBuilder SetAvailabilities(IList<Availability> availabilities)
        {
            Availabilities = availabilities;
            return this;
        }

        public VirtualMachineBuilder SetBackupFrequenty(BackupFrequency backupFrequeny)
        {
            BackupFrequency = backupFrequeny;
            return this;
        }

        public VirtualMachineBuilder SetApplicationDate(DateTime applicationDate)
        {
            ApplicationDate = applicationDate;
            return this;
        }

        public VirtualMachineBuilder SetDuration(Duration timeSpan)
        {
            TimeSpan = timeSpan;
            return this;
        }

        public VirtualMachineBuilder SetStatus(Status status)
        {
            Status = status;
            return this;
        }

        public VirtualMachineBuilder SetReason(string reason)
        {
            Reason = reason;
            return this;
        }

        public VirtualMachineBuilder SetPorts(IList<Port> ports)
        {
            Ports = ports;
            return this;
        }

        public VirtualMachineBuilder SetHost(Host host)
        {
            Host = host;
            return this;
        }

        public VirtualMachineBuilder SetAccount(Account account)
        {
            Account = account;
            return this;
        }

        public VirtualMachineBuilder SetCredentials(IList<Credentials> credentials)
        {
            Credentials = credentials;
            return this;
        }

        public VirtualMachineBuilder SetRequester(Customer customer)
        {
            Requester = customer;
            return this;
        }

        public VirtualMachineBuilder SetUser(Customer user)
        {
            User = user;
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
