using Application.ServiceTypes.Queries;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Authorize]
[Route("api/service-types")]
public class ServiceTypesController : ControllerBase
{
    private readonly Mediator _mediator;

    public ServiceTypesController(Mediator mediator)
    {
        _mediator = Guard.Against.Null(mediator, nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetServiceTypesQuery());
        
        return StatusCode(result.StatusCode, result);
    }
}