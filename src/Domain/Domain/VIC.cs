using Domain.Args;
using Domain.Repositories;

namespace Domain.Domain;

public class VIC
{
    #region Singleton
    private static VIC _instance = new();

    private VIC()
    {
        VirtualMachineRepository = new VirtualMachineRepository();
        AccountRepository = new AccountRepository();
        CustomerRepository = new CustomerRepository();
    }

    public static VIC Instance => _instance;
    #endregion

    #region Properties
    public VirtualMachineRepository VirtualMachineRepository { get; }
    public AccountRepository AccountRepository { get; }
    public CustomerRepository CustomerRepository { get; }
    #endregion

    #region Methods VirtualMachine
    internal void CreateVirtualMachine(VirtualMachineArgs args)
    {
        HashSet<Port> ports = new();
        HashSet<Credentials> credentials = new();
        if (args.ports != null)
        {
            foreach (var keyset in args.ports)
            {
                ports.Add(new Port(keyset.Key, keyset.Value));
            }
        }
        if (args.credentials != null)
        {
            foreach (var keyset in args.credentials)
            {
                credentials.Add(new Credentials(keyset.Key, keyset.Value));
            }
        }

        VirtualMachine vm = new VirtualMachine.VirtualMachineBuilder()
            .SetName(args.name)
            .SetResource(new Resources(args.processors, args.memory, args.storage))
            .SetTemplate(args.template)
            .SetMode(args.mode)
            .SetFqdn(args.fqdn)
            .SetAvailabilities(args.availabilities)
            .SetBackupFrequenty(args.backupFrequenty)
            .SetApplicationDate(args.applicationDate)
            // TODO: test dit via gui of het werkt
            //.SetDuration(new Duration(args.duration.First(), args.duration.Last()))
            .SetStatus(args.status)
            .SetReason(args.reason)
            .SetPorts(ports)
            .SetCredentials(credentials)
            .SetAccount(AccountRepository.GetAccountByEmail(args.accountEmail))
            // TODO: Use id instead of email.
            //.SetRequester(CustomerRepository.GetCustomerByEmail(args.requesterEmail))
            //.SetUser(CustomerRepository.GetCustomerByEmail(args.userEmail))
            .Build();

        // TODO: pas implementeren als server repo gemaakt is
        //.SetHost() --> op naam vinden

        VirtualMachineRepository.Add(vm);
    }

    #endregion

    #region Methods Account
    public void CreateAccount(AccountArgs args)
    {
        Account account =
            new(
                args.Firstname,
                args.Lastname,
                args.Email,
                args.Role,
                args.Password,
                args.Department,
                args.Education
            );
        AccountRepository.Add(account);
    }
    #endregion

    #region Methods Customer
    public void CreateInternalCustomer(CustomerArgs args)
    {
        InternalCustomer customer =
            new(
                args.Education,
                args.Department,
                new ContactPerson(args.Firstname, args.Lastname, args.Email, args.PhoneNumber),
                new ContactPerson(
                    args.BackupFirstname,
                    args.BackupLastname,
                    args.BackupEmail,
                    args.BackupPhoneNumber
                )
            );
        CustomerRepository.Add(customer);
    }

    public void CreateExternalCustomer(CustomerArgs args)
    {
        ExternalCustomer customer =
            new(
                args.Name,
                args.Type,
                new ContactPerson(args.Firstname, args.Lastname, args.Email, args.PhoneNumber),
                new ContactPerson(
                    args.BackupFirstname,
                    args.BackupLastname,
                    args.BackupEmail,
                    args.BackupPhoneNumber
                )
            );
        CustomerRepository.Add(customer);
    }
    #endregion
}
