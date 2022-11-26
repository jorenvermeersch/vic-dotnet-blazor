using Microsoft.AspNetCore.Mvc;
using Shared.Customers;

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
}



