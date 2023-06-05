using Application.MedicalCards.Queries;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Authorize]
[Route("api/medical-cards")]
public class MedicalCardsController : ControllerBase
{
    private readonly Mediator _mediator;

    public MedicalCardsController(Mediator mediator)
    {
        _mediator = Guard.Against.Null(mediator, nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetMedicalCardsQuery());
        
        return StatusCode(result.StatusCode, result);
    }
}
