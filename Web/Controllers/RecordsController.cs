using Application.Records.Queries;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Authorize]
[Route("api/records")]
public class RecordsController : ControllerBase
{
    private readonly Mediator _mediator;

    public RecordsController(Mediator mediator)
    {
        _mediator = Guard.Against.Null(mediator, nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> Get(long medicalCardId)
    {
        var result = await _mediator.Send(new GetRecordsQuery() {MedicalCardId = medicalCardId});
        
        return StatusCode(result.StatusCode, result);
    }
}
