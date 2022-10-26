using Domain.Args;
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
    public AccountRepo AccountRepo { get; }
    public CustomerRepo CustomerRepo { get; }
    #endregion
    #region VirtualMachine methods
    internal void CreateVirtualMachine(VirtualMachineArgs args) {
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
            .SetAccount(AccountRepo.GetAccountByEmail(args.accountEmail))
            .SetRequester(CustomerRepo.GetCustomerByEmail(args.requesterEmail))
            .SetUser(CustomerRepo.GetCustomerByEmail(args.userEmail))
            .Build();

        // TODO: pas implementeren als server repo gemaakt is
        //.SetHost() --> op naam vinden

        VirtualMachineRepo.AddMachine(vm);
    }


    #endregion

    #region Account methods
    internal void CreateAccount(AccountArgs args)
    {
        Account account = new(args.Firstname, args.Lastname, args.Email, args.Role, args.Password, args.Department, args.Education);
        AccountRepo.AddAccount(account);
    }
    #endregion

    #region Customer methods
    internal void CreateInternalCustomer(CustomerArgs args)
    {
        InternalCustomer customer = new(args.Education, args.Department, new ContactPerson(args.Firstname, args.Lastname, args.Email, args.PhoneNumber), new ContactPerson(args.BackupFirstname,args.BackupLastname, args.BackupEmail, args.BackupPhoneNumber));
        CustomerRepo.AddInternalCustomer(customer);
    }

    internal void CreateExternalCustomer(CustomerArgs args)
    {
        ExternalCustomer customer = new(args.Name, args.Type, new ContactPerson(args.Firstname, args.Lastname, args.Email, args.PhoneNumber), new ContactPerson(args.BackupFirstname, args.BackupLastname, args.BackupEmail, args.BackupPhoneNumber));
        CustomerRepo.AddExternalCustomer(customer);
    }
    #endregion

}