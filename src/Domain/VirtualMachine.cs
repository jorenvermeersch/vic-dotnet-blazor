namespace Domain;
public class VirtualMachine : IVirtualMachine {
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
    }
    public class VirtualMachineBuilder {
        private string _name;
        private int _vcpu;
        private int _memory;
        private int _storage;
        private Template _template;
        private Mode _mode;
        private string _fqdn;
        private HashSet<Availability> _availabilities = new();
        private BackupFrequenty _backupFrequenty;
        private DateTime _applicationDate;
        private Duration _duration;
        private Status _status;
        private string _reason;
        private HashSet<Port> _ports = new();

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
        public VirtualMachineBuilder SetAvailabilities(HashSet<Availability> availabilities) {
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
        public VirtualMachineBuilder SetPorts(HashSet<Port> ports) {
            _ports = ports;
            return this;
        }
        public VirtualMachine Build() {
            return new VirtualMachine(this);
        }
    }

}