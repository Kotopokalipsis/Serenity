﻿using Application.Common.Interfaces.Application.Responses;
using Application.Common.Interfaces.Application.Services;
using Application.Common.Interfaces.Infrastructure.UnitOfWork;
using Application.Common.Responses;
using Application.Common.Services.ResponseHelper;
using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Models;
using MediatR;

namespace Application.Tokens.Queries;

public record NewRefreshTokenQuery : IRequest<IBaseResponse<Token>>;

public class NewRefreshTokenHandler : IRequestHandler<NewRefreshTokenQuery, IBaseResponse<Token>>
{
    private readonly ITokenHelper _tokenHelper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICookieHelper _cookieHelper;

    public NewRefreshTokenHandler(ITokenHelper tokenHelper, IUnitOfWork unitOfWork, ICookieHelper cookieHelper)
    {
        _tokenHelper = Guard.Against.Null(tokenHelper, nameof(tokenHelper));
        _unitOfWork = Guard.Against.Null(unitOfWork, nameof(unitOfWork));
        _cookieHelper = Guard.Against.Null(cookieHelper, nameof(cookieHelper));
    }

    public async Task<IBaseResponse<Token>> Handle(NewRefreshTokenQuery request, CancellationToken ct)
    {
        var user = await _tokenHelper.GetUserByRefreshToken();

        if (user == null) return ResponseHelper<Token>.GetRefreshTokenErrorResponse();
        
        await _tokenHelper.BanRefreshToken();

        var newRefreshToken = await _tokenHelper.GenerateNewRefreshToken(user);
        
        user.RefreshTokenExpirationTime = DateTime.Now.ToUniversalTime();
        await _unitOfWork.Commit(ct);
        
        _cookieHelper.SetRefreshTokenInCookie(newRefreshToken);

        return new BaseResponse<Token>
        {
            StatusCode = 200,
            Data = null
        };
    }
}
