using Domain.Customers;
using Persistence.Data;
using Shared.Customers;
using Microsoft.EntityFrameworkCore;
using Domain.Constants;
using Shared.VirtualMachines;

namespace Services.Customers;

public class CustomerService : ICustomerService
{
    private readonly VicDBContext _dbContext;
    private readonly DbSet<Customer> _customers;

    private IQueryable<Customer> GetCustomerById(long id) => _customers
                .AsNoTracking()
                .Where(p => p.Id == id);

    public CustomerService(VicDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CustomerResponse.Create> CreateAsync(CustomerRequest.Create request)
    {
        CustomerResponse.Create response = new();
        Customer customer;
        CustomerDto.Mutate createdCustomer = request.Customer;

        ContactPerson contactperson = new(createdCustomer.ContactPerson.Firstname, createdCustomer.ContactPerson.Lastname, createdCustomer.ContactPerson.Email, createdCustomer.ContactPerson.Phonenumber);
        ContactPerson backupContactperson = null;
        if (string.IsNullOrEmpty(createdCustomer.BackupContactPerson.Firstname))
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
                Id = _customers.Max(x => x.Id) + 1
            };
        }
        else
        {
            customer = new ExternalCustomer(
               createdCustomer.CompanyName,
               createdCustomer.CompanyType,
               contactperson,
               backupContactperson
               );
        }
        
        var addedCustomer = _customers.Add(customer);
        await _dbContext.SaveChangesAsync();

        response.CustomerId = addedCustomer.Entity.Id;

        return response;
    }

    public Task DeleteAsync(CustomerRequest.Delete request)
    {
        throw new NotImplementedException();
    }

    public async Task<CustomerResponse.Edit> EditAsync(CustomerRequest.Edit request)
    {
        CustomerResponse.Edit response = new();
        var customer = await GetCustomerById(request.CustomerId).SingleOrDefaultAsync();

        if(customer is not null)
        {
            ContactPerson contactPerson = new ContactPerson(request.Customer.ContactPerson.Firstname, request.Customer.ContactPerson.Lastname, request.Customer.ContactPerson.Email, request.Customer.ContactPerson.Email);
            ContactPerson backupContactPerson = new ContactPerson(request.Customer.BackupContactPerson.Firstname, request.Customer.BackupContactPerson.Lastname, request.Customer.BackupContactPerson.Email, request.Customer.BackupContactPerson.Email);


            if (customer is InternalCustomer)
            {
                InternalCustomer inCus = (InternalCustomer)customer;
                inCus.Institution = (Institution)Enum.Parse(typeof(Institution), request.Customer.Institution, true);
                inCus.Education = request.Customer.Education;
                inCus.Department = request.Customer.Department;
                inCus.ContactPerson = contactPerson;
                if (request.Customer.BackupContactPerson.Firstname != "")
                {
                    inCus.BackupContactPerson = backupContactPerson;
                }
            }
            else
            {
                ExternalCustomer exCus = (ExternalCustomer)customer;
                exCus.CompanyName = request.Customer.CompanyName;
                exCus.Type = request.Customer.CompanyType;
                exCus.ContactPerson = contactPerson;
                if (request.Customer.BackupContactPerson.Firstname != "")
                {
                    exCus.BackupContactPerson = backupContactPerson;
                }
            }

            _dbContext.Entry(customer).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            response.CustomerId = customer.Id;
        }

        return response;
        
    }

    public async Task<CustomerResponse.GetDetail> GetDetailAsync(CustomerRequest.GetDetail request)
    {
        CustomerResponse.GetDetail response = new();
        throw new NotImplementedException();
    }

    public Task<CustomerResponse.GetIndex> GetIndexAsync(CustomerRequest.GetIndex request)
    {
        throw new NotImplementedException();
    }
}
