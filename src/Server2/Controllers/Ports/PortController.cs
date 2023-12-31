﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Ports;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Ports;

[ApiController]
[Route("/api/ports/")]
[Authorize]
public class PortController : ControllerBase
{
    private readonly IPortService portService;

    public PortController(IPortService portService)
    {
        this.portService = portService;
    }

    [SwaggerOperation("Returns a list of ports")]
    [HttpGet]
    public async Task<PortResponse.GetAll> GetIndex()
    {
        PortResponse.GetAll response = await portService.GetAllAsync(new PortRequest.GetAll());
        return response;
    }
}



