using Microsoft.AspNetCore.Mvc;
using Shared.Customers;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Customers;

[ApiController]
[Route("/api/customers/")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService customerService;

    public CustomerController(ICustomerService customerService)
    {
        this.customerService = customerService;
    }

    [SwaggerOperation("Returns a list of all the customers.")]
    [HttpGet]
    public async Task<CustomerResponse.GetIndex> GetIndex([FromQuery] CustomerRequest.GetIndex request)
    {
        return await customerService.GetIndexAsync(request);
    }

    [SwaggerOperation("Returns the details of the customer with the given customerID.")]
    [HttpGet("{CustomerId}")]
    public async Task<CustomerResponse.GetDetail> GetDetails([FromRoute] CustomerRequest.GetDetail request)
    {
        return await customerService.GetDetailAsync(request);
    }

    [SwaggerOperation("Creates a new customer.")]
    [HttpPost]
    public async Task<IActionResult> Create(CustomerRequest.Create request)
    {
        CustomerResponse.Create customer =  await customerService.CreateAsync(request);
        return CreatedAtAction(nameof(Create), customer);
    }

    [SwaggerOperation("Edites an existing customer.")]
    [HttpPut]
    public async Task<CustomerResponse.Edit> Edit(CustomerRequest.Edit request)
    {
        return await customerService.EditAsync(request);
    }
}



