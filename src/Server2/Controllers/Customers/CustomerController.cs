using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Customers;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Customers;

[ApiController]
[Route("/api/customers/")]
[Authorize]
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

    [SwaggerOperation("Returns the details of all customers.")]
    [HttpGet("alldetails")]
    [AllowAnonymous]
    public async Task<CustomerResponse.GetAllDetail> GetAllDetails([FromRoute] CustomerRequest.GetAllDetails request)
    {
        return await customerService.GetAllDetailAsync(request);
    }

    [SwaggerOperation("Creates a new customer.")]
    [HttpPost]
    [Authorize(Roles = "Admin, Master")]
    public async Task<IActionResult> Create(CustomerRequest.Create request)
    {
        CustomerResponse.Create customer =  await customerService.CreateAsync(request);
        return CreatedAtAction(nameof(Create), customer);
    }

    [SwaggerOperation("Edits an existing customer.")]
    [HttpPut]
    [Authorize(Roles = "Admin, Master")]
    public async Task<IActionResult> EditAsync(CustomerRequest.Edit request)
    {
        var response = await customerService.EditAsync(request);
        return Accepted(nameof(EditAsync), response);
    }

    [SwaggerOperation("Deletes an existing customer.")]
    [HttpDelete("{CustomerId}")]
    [Authorize(Roles = "Admin, Master")]
    public async Task<IActionResult> Delete([FromRoute] CustomerRequest.Delete request)
    {
        await customerService.DeleteAsync(request);
        return NoContent();
    }
}



