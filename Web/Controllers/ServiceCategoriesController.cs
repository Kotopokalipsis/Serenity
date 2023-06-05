using Application.ServiceCategories.Queries;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Authorize]
[Route("api/service-categories")]
public class ServiceCategoriesController : ControllerBase
{
    private readonly Mediator _mediator;

    public ServiceCategoriesController(Mediator mediator)
    {
        _mediator = Guard.Against.Null(mediator, nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetServiceCategoriesQuery());
        
        return StatusCode(result.StatusCode, result);
    }
}