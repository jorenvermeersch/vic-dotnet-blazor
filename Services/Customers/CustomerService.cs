using Domain.Constants;
using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Shared.Customers;
using Shared.VirtualMachines;

namespace Services.Customers;

public class CustomerService : ICustomerService
{

    private readonly VicDBContext _dbContext;
    private readonly DbSet<Customer> _customers;

    private IQueryable<Customer> GetCustomerById(long id)
    {
        return _customers
                .AsNoTracking()
                .Where(p => p.Id == id);
    }

    public CustomerService(VicDBContext dbContext)
    {
        _dbContext = dbContext;
        _customers = _dbContext.Customers;
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

        if (createdCustomer.CustomerType == CustomerType.Intern)
        {
            customer = new InternalCustomer(
                createdCustomer.Institution!.Value,
                createdCustomer.Department,
                createdCustomer.Education,
                contactperson,
                backupContactperson
                );
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

    public async Task DeleteAsync(CustomerRequest.Delete request)
    {
        _customers.RemoveIf(customer => customer.Id == request.CustomerId);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<CustomerResponse.Edit> EditAsync(CustomerRequest.Edit request)
    {
        CustomerResponse.Edit response = new();
        var customer = await GetCustomerById(request.CustomerId).SingleOrDefaultAsync();

        if (customer is not null)
        {
            ContactPerson contactPerson = new(request.Customer.ContactPerson.Firstname, request.Customer.ContactPerson.Lastname, request.Customer.ContactPerson.Email, request.Customer.ContactPerson.Phonenumber);
            ContactPerson backupContactPerson = new(request.Customer.BackupContactPerson.Firstname, request.Customer.BackupContactPerson.Lastname, request.Customer.BackupContactPerson.Email, request.Customer.BackupContactPerson.Phonenumber);


            if (customer is InternalCustomer)
            {
                InternalCustomer inCus = (InternalCustomer)customer;
                inCus.Institution = request.Customer.Institution!.Value;
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

        Customer customer = await GetCustomerById(request.CustomerId).SingleOrDefaultAsync();

        ContactPersonDto backup = null;
        if (customer.BackupContactPerson is not null && !string.IsNullOrEmpty(customer.BackupContactPerson.Firstname))
        {
            backup = new ContactPersonDto
            {
                Firstname = customer.BackupContactPerson.Firstname,
                Lastname = customer.BackupContactPerson.Lastname,
                Email = customer.BackupContactPerson.Email,
                Phonenumber = customer.BackupContactPerson.PhoneNumber
            };
        }

        CustomerDto.Detail model = new()
        {
            Id = customer.Id,
            ContactPerson = new ContactPersonDto
            {
                Firstname = customer.ContactPerson.Firstname,
                Lastname = customer.ContactPerson.Lastname,
                Email = customer.ContactPerson.Email,
                Phonenumber = customer.ContactPerson.PhoneNumber
            },
            BackupContactPerson = backup,
            VirtualMachines = new List<VirtualMachineDto.Index>() { }, //TODO
        };

        if (customer is ExternalCustomer ex)
        {
            model.CustomerType = CustomerType.Extern;
            model.CompanyType = ex.Type;
            model.CompanyName = ex.CompanyName;
        }
        else
        {
            InternalCustomer intern = (InternalCustomer)customer;
            model.CustomerType = CustomerType.Intern;
            model.Department = intern.Department;
            model.Institution = intern.Institution;
            model.Education = intern.Education;
        }

        response.Customer = model;


        return response;
    }

    public async Task<CustomerResponse.GetIndex> GetIndexAsync(CustomerRequest.GetIndex request)
    {
        CustomerResponse.GetIndex response = new();
        var query = _customers.AsQueryable().AsNoTracking();



        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(x => x.ContactPerson.Firstname.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)
                                  );
        }
        if (!string.IsNullOrWhiteSpace(request.CustomerType))
        {
            query = query.Where(x => request.CustomerType == "intern" ? x is InternalCustomer : x is ExternalCustomer);
        }

        response.TotalAmount = query.Count();

        query = query.Skip(request.Offset);
        query = query.Take(request.Amount);

        response.Customers = await query.Select(x => new CustomerDto.Index
        {
            Id = x.Id,
            Name = String.Format("{0} {1}", x.ContactPerson.Firstname, x.ContactPerson.Lastname),
            Email = x.ContactPerson.Email,
            CustomerType = x is ExternalCustomer ? CustomerType.Extern : CustomerType.Intern

        }).ToListAsync();

        return response;
    }
}

