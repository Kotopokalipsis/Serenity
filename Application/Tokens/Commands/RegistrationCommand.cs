using Application.Common.Interfaces.Application.Responses;
using Application.Common.Interfaces.Infrastructure.UnitOfWork;
using Application.Common.Responses;
using Application.Common.Services.ResponseHelper;
using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Tokens.Commands;

public record RegistrationCommand : IRequest<IBaseResponse<Token>>
{
    public string UserName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string Firstname { get; init; }
    public string Middlename { get; init; }
    public string Lastname { get; init; }
    public string Country { get; init; }
}

public class RegistrationHandler : IRequestHandler<RegistrationCommand, IBaseResponse<Token>>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public RegistrationHandler(
        UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _unitOfWork = Guard.Against.Null(unitOfWork, nameof(unitOfWork));
        _userManager = Guard.Against.Null(userManager, nameof(userManager));
    }

    public async Task<IBaseResponse<Token>> Handle(RegistrationCommand request, CancellationToken ct)
    {
        var user = new User
        {
            Email = request.Email,
            UserName = request.UserName
        };

        var existedEmail = await _userManager.FindByEmailAsync(request.Email);
        
        if (existedEmail != null)
        {
            return ResponseHelper<Token>.GetEmailIsAlreadyUsedErrorResponse();
        }
        
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return new ErrorResponse<Token>
            {
                StatusCode = 400,
                Errors = new Dictionary<string, List<string>>{{"CreateError", result.Errors.Select(x => x.Description.ToString()).ToList()}},
            };
        }

        user.CreationDate = DateTime.Now.ToUniversalTime();
        user.EmailConfirmed = true;

        _unitOfWork.ProfileRepository.Add(new Profile()
        {
            User = user,
            Firstname = request.Firstname,
            Middlename = request.Middlename,
            Lastname = request.Lastname,
            Country = request.Country,
        });
        
        await _unitOfWork.Commit(ct);

        return new BaseResponse<Token>
        {
            StatusCode = 201,
        };
    }
}
