using Application.Common.Interfaces.Application.Responses;
using Application.Common.Interfaces.Application.Services;
using Application.Common.Interfaces.Infrastructure.Services;
using Application.Common.Interfaces.Infrastructure.UnitOfWork;
using Application.Common.Responses;
using Application.Common.Services.ResponseHelper;
using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Tokens.Queries;

public record AccessTokenQuery : IRequest<IBaseResponse<Token>>;

public class AccessTokenHandler : IRequestHandler<AccessTokenQuery, IBaseResponse<Token>>
{
    private readonly ITokenHelper _tokenHelper;

    public AccessTokenHandler(ITokenHelper tokenHelper)
    {
        _tokenHelper = Guard.Against.Null(tokenHelper, nameof(tokenHelper));
    }
    
    public async Task<IBaseResponse<Token>> Handle(AccessTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _tokenHelper.GetUserByRefreshToken();

        if (user == null) return ResponseHelper<Token>.GetRefreshTokenErrorResponse();

        var accessToken = await _tokenHelper.GenerateNewAccessToken(user);

        return new BaseResponse<Token>
        {
            StatusCode = 200,
            Data = new Token {AccessToken = accessToken}
        };
    }
}