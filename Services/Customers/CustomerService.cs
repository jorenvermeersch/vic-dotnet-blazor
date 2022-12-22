using Azure.Core;
using Domain.Constants;
using Domain.Customers;
using Domain.Exceptions;
using Domain.VirtualMachines;
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
        throw new NotImplementedException();
    }

    public async Task<CustomerResponse.Edit> EditAsync(CustomerRequest.Edit request)
    {
        throw new NotImplementedException();

    }

    public async Task<CustomerResponse.GetDetail> GetDetailAsync(CustomerRequest.GetDetail request)
    {
        CustomerResponse.GetDetail response = new();
        Customer? customer = await GetCustomerById(request.CustomerId)
            .Include(x => x.ContactPerson)
            .Include(x => x.BackupContactPerson)
            .SingleOrDefaultAsync();

        if (customer is null)
        {
            throw new EntityNotFoundException(nameof(Customer), request.CustomerId);
        }

        // Fetch virtual machines of customer. 
        List<VirtualMachine>? machines = await dbContext.VirtualMachines
            .Where(machine => machine.Requester.Id == request.CustomerId || machine.User.Id == request.CustomerId)
            .ToListAsync();

        List<VirtualMachineDto.Index>? machineIndexes = null;

        if (machines is not null)
        {
            machineIndexes = machines.Select(machine => new VirtualMachineDto.Index()
            {
                Id = machine.Id,
                Fqdn = machine.Fqdn,
                Status = machine.Status,
            }).ToList();
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
            VirtualMachines = machineIndexes, 
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
            var searchTerm = request.SearchTerm.ToLower();
            query = query.Where(x => x.ContactPerson.Firstname.Contains(searchTerm) || x.ContactPerson.Lastname.Contains(searchTerm));
        }

        // CustomerType. 
        var internalCustomer = CustomerType.Intern.ToString().ToLower();

        if (!string.IsNullOrWhiteSpace(request.CustomerType))
        {
            query = query.Where(x => request.CustomerType.ToLower() == internalCustomer ? x is InternalCustomer : x is ExternalCustomer);
        }

        response.TotalAmount = query.Count();

        query = query.OrderByDescending(x => x.CreatedAt);
        query = query.Skip((request.Page - 1) * request.Amount);
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

    private static CustomerDto.Detail ToCustomerDetail(Customer customer)
    {

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

        CustomerDto.Detail customerDetails = new()
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
            VirtualMachines = new List<VirtualMachineDto.Index>()
        };

        if (customer is ExternalCustomer externalCustomer)
        {
            customerDetails.CustomerType = CustomerType.Extern;
            customerDetails.CompanyType = externalCustomer.Type;
            customerDetails.CompanyName = externalCustomer.CompanyName;
        }
        else if (customer is InternalCustomer internalCustomer)
        {
            customerDetails.CustomerType = CustomerType.Intern;
            customerDetails.Department = internalCustomer.Department;
            customerDetails.Institution = internalCustomer.Institution;
            customerDetails.Education = internalCustomer.Education;
        }


        return customerDetails;
    }

    public async Task<CustomerResponse.GetAllDetail> GetAllDetailAsync(CustomerRequest.GetAllDetails request)
    {
        CustomerResponse.GetAllDetail response = new();
        var query = customers.AsQueryable()
            .Include(x => x.ContactPerson)
            .Include(x => x.BackupContactPerson)
            .AsNoTracking();

        List<CustomerDto.Detail> customerList = await query.Select(customer => ToCustomerDetail(customer)).ToListAsync();

        //fetch virtual machine and add to customer
        foreach (var c in customerList)
        {
            List<VirtualMachine>? machines = await dbContext.VirtualMachines
                .Where(machine => machine.Requester.Id == c.Id || machine.User.Id == c.Id)
                .ToListAsync();
            List<VirtualMachineDto.Index>? machineIndexes = null;

            if (machines is not null)
            {
                machineIndexes = machines.Select(machine => new VirtualMachineDto.Index()
                {
                    Id = machine.Id,
                    Fqdn = machine.Fqdn,
                    Status = machine.Status,
                }).ToList();
            }

            c.VirtualMachines = machineIndexes;
        }

        response.Customers = customerList;
        response.TotalAmount = query.Count();

        return response;
    }
}

