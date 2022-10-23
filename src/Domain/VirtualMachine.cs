namespace Domain;
public class VirtualMachine : IVirtualMachine {
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; }
    public int VCpu { get; set; }
    public int GbMemory { get; set; }
    public int GbStorage { get; set; }
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
    #endregion
    #region Constructors
    public VirtualMachine(VirtualMachineBuilder b) {
        Name = b.Name;
        VCpu = b.VCpu;
        GbMemory = b.GbMemory;
        GbStorage = b.GbStorage;
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
    }
    #endregion
    #region Builder
    public class VirtualMachineBuilder {
        #region Fields
        private string _name;
        private int _vcpu;
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
        #endregion
        #region Properties
        public string Name => _name;
        public int VCpu => _vcpu;
        public int GbMemory => _memory;
        public int GbStorage => _storage;
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
        #endregion
        #region Methods
        public VirtualMachineBuilder SetName(string name) {
            _name = name;
            return this;
        }
        public VirtualMachineBuilder SetVCpu(int cpu) {
            _vcpu = cpu;
            return this;
        }
        public VirtualMachineBuilder SetMemory(int memory) {
            _memory = memory;
            return this;
        }
        public VirtualMachineBuilder SetStorage(int storage) {
            _storage = storage;
            return this;
        }
        public VirtualMachineBuilder SetTemplate(Template template) {
            _template = template;
            return this;
        }
        public VirtualMachineBuilder SetMode(Mode mode) {
            _mode = mode;
            return this;
        }
        public VirtualMachineBuilder SetFqdn(string fqdn) {
            _fqdn = fqdn;
            return this;
        }
        public VirtualMachineBuilder SetAvailabilities(ISet<Availability> availabilities) {
            _availabilities = availabilities;
            return this;
        }
        public VirtualMachineBuilder SetBackupFrequenty(BackupFrequenty backupFrequenty) {
            _backupFrequenty = backupFrequenty;
            return this;
        }
        public VirtualMachineBuilder SetApplicationDate(DateTime applicationDate) {
            _applicationDate = applicationDate;
            return this;
        }
        public VirtualMachineBuilder SetDuration(Duration duration) {
            _duration = duration;
            return this;
        }
        public VirtualMachineBuilder SetStatus(Status status) {
            _status = status;
            return this;
        }
        public VirtualMachineBuilder SetReason(string reason) {
            _reason = reason;
            return this;
        }
        public VirtualMachineBuilder SetPorts(ISet<Port> ports) {
            _ports = ports;
            return this;
        }
        public VirtualMachineBuilder SetHost(IHost host) {
            _host = host;
            return this;
        }
        public VirtualMachineBuilder SetAccount(Account account) {
            _account = account;
            return this;
        }
        public VirtualMachineBuilder SetCredentials(ISet<Credential> credentials) {
            _credentials = credentials;
            return this;
        }
        public VirtualMachine Build() {
            return new VirtualMachine(this);
        }
        #endregion
    }
    #endregion
}