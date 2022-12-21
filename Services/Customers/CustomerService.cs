using Domain.Constants;
using Domain.Customers;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Shared.Customers;
using Shared.VirtualMachines;

namespace Services.Customers;

public class CustomerService : ICustomerService
{

    private readonly VicDbContext dbContext;
    private readonly DbSet<Customer> customers;

    private IQueryable<Customer> GetCustomerById(long id)
    {
        return customers
                .AsNoTracking()
                .Where(p => p.Id == id);
    }

    public CustomerService(VicDbContext dbContext)
    {
        this.dbContext = dbContext;
        customers = this.dbContext.Customers;
    }

    public async Task<CustomerResponse.Create> CreateAsync(CustomerRequest.Create request)
    {
        CustomerResponse.Create response = new();
        Customer customer;
        CustomerDto.Mutate requested = request.Customer;

        ContactPerson contactperson = new(
            requested.ContactPerson.Firstname!,
            requested.ContactPerson.Lastname!,
            requested.ContactPerson.Email!,
            requested.ContactPerson.Phonenumber
            );

        ContactPerson? backupContactperson = null;

        if (requested.BackupContactPerson is not null)
        {
            backupContactperson = new(
                requested.BackupContactPerson.Firstname!,
                requested.BackupContactPerson.Lastname!,
                requested.BackupContactPerson.Email!,
                requested.BackupContactPerson.Phonenumber
                );
        }

        if (requested.CustomerType == CustomerType.Intern)
        {
            customer = new InternalCustomer(
                requested.Institution!.Value,
                requested.Department!,
                requested.Education!,
                contactperson,
                backupContactperson
                );
        }
        else
        {
            customer = new ExternalCustomer(
               requested.CompanyName!,
               requested.CompanyType!,
               contactperson,
               backupContactperson
               );
        }

        var createdCustomer = customers.Add(customer);
        await dbContext.SaveChangesAsync();

        response.CustomerId = createdCustomer.Entity.Id;

        return response;
    }

    public async Task DeleteAsync(CustomerRequest.Delete request)
    {
        customers.RemoveIf(customer => customer.Id == request.CustomerId);
        await dbContext.SaveChangesAsync();
    }

    public async Task<CustomerResponse.Edit> EditAsync(CustomerRequest.Edit request)
    {
        CustomerResponse.Edit response = new();
        var customer = await GetCustomerById(request.CustomerId).SingleOrDefaultAsync();

        if (customer is null)
        {
            throw new EntityNotFoundException(nameof(Customer), request.CustomerId);
        }

        ContactPerson contactPerson = new(
               request.Customer.ContactPerson.Firstname!,
               request.Customer.ContactPerson.Lastname!,
               request.Customer.ContactPerson.Email!,
               request.Customer.ContactPerson.Phonenumber
               );


        ContactPerson? backupContactPerson = null;

        if (request.Customer.BackupContactPerson is not null)
        {
            backupContactPerson = new(
                request.Customer.BackupContactPerson.Firstname!,
                request.Customer.BackupContactPerson.Lastname!,
                request.Customer.BackupContactPerson.Email!,
                request.Customer.BackupContactPerson.Phonenumber
            );
        }


        customer.ContactPerson = contactPerson;
        customer.BackupContactPerson = backupContactPerson;

        if (customer is InternalCustomer internalCustomer)
        {

            internalCustomer.Institution = request.Customer.Institution!.Value;
            internalCustomer.Education = request.Customer.Education!;
            internalCustomer.Department = request.Customer.Department!;

        }
        else if (customer is ExternalCustomer externalCustomer)
        {
            externalCustomer.CompanyName = request.Customer.CompanyName!;
            externalCustomer.Type = request.Customer.CompanyType!;
        }

        dbContext.Entry(customer).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
        response.CustomerId = customer.Id;

        return response;

    }

    public async Task<CustomerResponse.GetDetail> GetDetailAsync(CustomerRequest.GetDetail request)
    {
        CustomerResponse.GetDetail response = new();
        Customer? customer = await GetCustomerById(request.CustomerId).SingleOrDefaultAsync();

        if (customer is null)
        {
            throw new EntityNotFoundException(nameof(Customer), request.CustomerId);
        }

        ContactPersonDto? backupContact = null;

        if (customer.BackupContactPerson is not null)
        {
            backupContact = new ContactPersonDto
            {
                Firstname = customer.BackupContactPerson.Firstname!,
                Lastname = customer.BackupContactPerson.Lastname!,
                Email = customer.BackupContactPerson.Email!,
                Phonenumber = customer.BackupContactPerson.PhoneNumber
            };
        }


        var virtualMachines = customer.VirtualMachines.Select(machine =>
            new VirtualMachineDto.Index()
            {
                Id = machine.Id,
                Fqdn = machine.Fqdn,
                Status = machine.Status,
            }
        ).ToList();

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
            BackupContactPerson = backupContact,
            VirtualMachines = virtualMachines, // TODO: Do you need to fetch virtual machines explicitly. 
        };

        if (customer is ExternalCustomer externalCustomer)
        {
            model.CustomerType = CustomerType.Extern;
            model.CompanyType = externalCustomer.Type;
            model.CompanyName = externalCustomer.CompanyName;
        }
        else if (customer is InternalCustomer internalCustomer)
        {
            model.CustomerType = CustomerType.Intern;
            model.Department = internalCustomer.Department;
            model.Institution = internalCustomer.Institution;
            model.Education = internalCustomer.Education;
        }

        response.Customer = model;
        return response;
    }

    public async Task<CustomerResponse.GetIndex> GetIndexAsync(CustomerRequest.GetIndex request)
    {
        CustomerResponse.GetIndex response = new();
        var query = customers.AsQueryable().AsNoTracking();

        // Searchterm. 
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(x => x.ContactPerson.Firstname.Contains(request.SearchTerm));
        }

        // CustomerType. 
        var internalCustomer = CustomerType.Intern.ToString().ToLower();

        if (!string.IsNullOrWhiteSpace(request.CustomerType))
        {
            query = query.Where(x => request.CustomerType.ToLower() == internalCustomer ? x is InternalCustomer : x is ExternalCustomer);
        }

        response.TotalAmount = query.Count();

        query = query.Skip(request.Offset);
        query = query.Take(request.Amount);

        response.Customers = await query.Select(x => new CustomerDto.Index
        {
            Id = x.Id,
            Name = $"{x.ContactPerson.Firstname} {x.ContactPerson.Lastname}",
            Email = x.ContactPerson.Email,
            CustomerType = x is ExternalCustomer ? CustomerType.Extern : CustomerType.Intern

        }).ToListAsync();

        return response;
    }
}

