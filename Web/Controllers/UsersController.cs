using Application.Users.Queries;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Authorize]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly Mediator _mediator;

    public UsersController(Mediator mediator)
    {
        _mediator = Guard.Against.Null(mediator, nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetUser(Guid guid)
    {
        var result = await _mediator.Send(new GetUserQuery() {UserId = guid});
        
        return StatusCode(result.StatusCode, result);
    }
}
