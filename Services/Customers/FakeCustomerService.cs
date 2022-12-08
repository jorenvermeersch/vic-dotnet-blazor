using Domain.Accounts;
using Domain.Constants;
using Domain.Customers;
using Services.FakeInitializer;
using Shared.Customers;
using Shared.VirtualMachines;

namespace Services.Customers;

public class FakeCustomerService : ICustomerService
{
    public static readonly List<Customer> customers = new();

    static FakeCustomerService()
    {
        customers = FakeInitializerService.FakeCustomers ?? new List<Customer>();
    }

    public async Task<CustomerResponse.Create> CreateAsync(CustomerRequest.Create request)
    {
        Customer customer;
        CustomerDto.Mutate createdCustomer = request.Customer;
        ContactPerson contactperson = new(createdCustomer.ContactPerson.Firstname, createdCustomer.ContactPerson.Lastname, createdCustomer.ContactPerson.Email, createdCustomer.ContactPerson.Phonenumber);
        ContactPerson backupContactperson=null;
        if ( !string.IsNullOrEmpty( createdCustomer.BackupContactPerson.Firstname))
        {
             backupContactperson = new(createdCustomer.BackupContactPerson.Firstname, createdCustomer.BackupContactPerson.Lastname, createdCustomer.BackupContactPerson.Email, createdCustomer.BackupContactPerson.Phonenumber);

        }


        if (createdCustomer.CustomerType.ToLower().Equals("intern"))
        {
            customer = new InternalCustomer(
                (Institution)Enum.Parse(typeof(Institution), createdCustomer.Institution, true),
                createdCustomer.Department,
                createdCustomer.Education,
                contactperson,
                backupContactperson
                
                )
            {
                Id = customers.Max(x => x.Id) + 1
            };
            
        }
        else
        {
            customer = new ExternalCustomer(
               createdCustomer.CompanyName,
               createdCustomer.CompanyType,
               contactperson,
               backupContactperson
               )
            {
                Id = customers.Max(x => x.Id) + 1
            };

        }

        customers.Add(customer);

        return new CustomerResponse.Create{
            CustomerId = customer.Id
        };
    }

    public async Task DeleteAsync(CustomerRequest.Delete request)
    {
        await Task.Delay(100);
        var customer = customers.Find(x => x.Id == request.CustomerId);
        customers.Remove(customer);
    }

    public async Task<CustomerResponse.Edit> EditAsync(CustomerRequest.Edit request)
    {
        await Task.Delay(100);
        Customer customer = customers.SingleOrDefault(x => x.Id == request.CustomerId);
        int index = customers.IndexOf(customer);

        ContactPerson contactPerson = new ContactPerson(request.Customer.ContactPerson.Firstname, request.Customer.ContactPerson.Lastname, request.Customer.ContactPerson.Email, request.Customer.ContactPerson.Phonenumber);
        ContactPerson backupContactPerson = null;
        if (request.Customer.BackupContactPerson is not null && !string.IsNullOrEmpty(request.Customer.BackupContactPerson.Firstname) )
        {
            backupContactPerson = new ContactPerson(request.Customer.BackupContactPerson.Firstname, request.Customer.BackupContactPerson.Lastname, request.Customer.BackupContactPerson.Email, request.Customer.BackupContactPerson.Phonenumber);
        }

        if (customer is InternalCustomer)
        {
            InternalCustomer inCus = (InternalCustomer)customer;
            inCus.Institution = (Institution)Enum.Parse(typeof(Institution), request.Customer.Institution, true);
            inCus.Education = request.Customer.Education;
            inCus.Department = request.Customer.Department;
            inCus.ContactPerson = contactPerson;
            inCus.BackupContactPerson = backupContactPerson;

            customers[index] = inCus;
        }
        else if(customer is ExternalCustomer)
        {
            ExternalCustomer exCus = (ExternalCustomer)customer;
            exCus.CompanyName = request.Customer.CompanyName;
            exCus.Type = request.Customer.CompanyType;
            exCus.ContactPerson = contactPerson;
            exCus.BackupContactPerson = backupContactPerson;

            customers[index] = exCus;
        }
        

        return new CustomerResponse.Edit
        {
            CustomerId = customer.Id
        };

    }

    public async Task<CustomerResponse.GetDetail> GetDetailAsync(CustomerRequest.GetDetail request)
    {
        CustomerResponse.GetDetail response = new();
        //.Where(x => customertype.ToLower() == "extern" ? x is ExternalCustomer : x is InternalCustomer)
        response.Customer = customers.Where(x => x.Id == request.CustomerId).Select(x =>
        {
            ContactPersonDto backup=null;
            if (x.BackupContactPerson is not null && !string.IsNullOrEmpty( x.BackupContactPerson.Firstname))
            {
                backup = new ContactPersonDto
                {
                    Firstname = x.BackupContactPerson.Firstname,
                    Lastname = x.BackupContactPerson.Lastname,
                    Email = x.BackupContactPerson.Email,
                    Phonenumber = x.BackupContactPerson.PhoneNumber
                };
            }
            
            //basic customer information
            CustomerDto.Detail customer = new()
            {
                Id = x.Id,
                ContactPerson = new ContactPersonDto
                {
                    Firstname = x.ContactPerson.Firstname,
                    Lastname = x.ContactPerson.Lastname,
                    Email = x.ContactPerson.Email,
                    Phonenumber = x.ContactPerson.PhoneNumber
                },
                BackupContactPerson = backup,
                VirtualMachines = x.VirtualMachines.Select(x =>new VirtualMachineDto.Index{
                   Id = x.Id,
                   Fqdn = x.Fqdn,
                   Status = x.Status
                }).ToList(),
            };

            //add the customertype specific information
            if (x is ExternalCustomer)
            {
                ExternalCustomer ex = (ExternalCustomer)x;
                customer.CustomerType = CustomerType.Extern;
                customer.CompanyType = ex.Type;
                customer.CompanyName = ex.CompanyName;
            }
            else
            {
                InternalCustomer intern = (InternalCustomer)x;
                customer.CustomerType = CustomerType.Intern;
                customer.Department = intern.Department;
                customer.Institution = intern.Institution;
                customer.Education = intern.Education;
            }

            return customer;
        }).First();


        return response;
    }

    public async Task<CustomerResponse.GetIndex> GetIndexAsync(CustomerRequest.GetIndex request)
    {
        CustomerResponse.GetIndex response = new();
        var query = customers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(x => x.ContactPerson.Firstname.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)
                                  || x.ContactPerson.Lastname.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrWhiteSpace(request.CustomerType))
        {
            query = query.Where(x => request.CustomerType == "intern" ? x is InternalCustomer : x is ExternalCustomer);
        }

        response.TotalAmount = query.Count();

        response.Customers = query.Skip((request.Page - 1) * request.Amount).Take(request.Amount).Select(x => new CustomerDto.Index
        {
            Id = x.Id,
            Name = String.Format("{0} {1}", x.ContactPerson.Firstname, x.ContactPerson.Lastname),
            Email = x.ContactPerson.Email,
            CustomerType = x is ExternalCustomer ? CustomerType.Extern : CustomerType.Intern

        }).ToList();

        return response;
    }
}
