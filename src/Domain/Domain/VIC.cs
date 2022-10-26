using Domain.Constants;
using Domain.Repositories;

namespace Domain.Domain;
public class VIC {
    #region Singleton pattern
    private static VIC _instance = new VIC();
    private VIC() {
    VirtualMachineRepo = new VirtualMachineRepo();
    }
    public static VIC Instance => _instance;
    #endregion
    #region Repositories
    public VirtualMachineRepo VirtualMachineRepo { get; }
    #endregion
    #region VirtualMachine methods
    public void CreateVirtualMachine(VirtualMachineArgs args) {
        HashSet<Port> ports = new();
        HashSet<Credential> credentials = new();
        foreach(var keyset in args.ports) {
            ports.Add(new Port(keyset.Key, keyset.Value));
        }
        foreach(var keyset in args.credentials) {
            credentials.Add(new Credential(keyset.Key, keyset.Value));
        }

        VirtualMachine vm = new VirtualMachine.VirtualMachineBuilder()
            .SetName(args.name)
            .SetResource(new Resource(args.processors, args.memory, args.storage))
            .SetTemplate(args.template)
            .SetMode(args.mode)
            .SetFqdn(args.fqdn)
            .SetAvailabilities(args.availabilities)
            .SetBackupFrequenty(args.backupFrequenty)
            .SetApplicationDate(args.applicationDate)
            .SetDuration(new Duration(args.duration.First(), args.duration.Last()))
            .SetStatus(args.status)
            .SetReason(args.reason)
            .SetPorts(ports)
            .SetCredentials(credentials)
            .Build();

        // TODO: Pas implementeren als account repo gemaakt is
        //.SetAccount() --> op email vinden

        // TODO: Pas implementeren als customer repo gemaakt is
        //.SetRequester() --> op email vinden

        // TODO: pas implementeren als server repo gemaakt is
        //.SetHost() --> op naam vinden

        VirtualMachineRepo.AddMachine(vm);
    }
    #endregion

}