﻿using Application.Common.Interfaces.Application.Responses;
using Application.Common.Interfaces.Application.Services;
using Application.Common.Interfaces.Infrastructure.UnitOfWork;
using Application.Common.Responses;
using Application.Common.Services.ResponseHelper;
using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Tokens.Commands;

public record LoginCommand : IRequest<IBaseResponse<Token>>
{
    public string Email { get; init; }
    public string Password { get; init; }
}

public class LoginHandler : IRequestHandler<LoginCommand, IBaseResponse<Token>>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ICookieHelper _cookieHelper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenHelper _tokenHelper;

    public LoginHandler(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IUnitOfWork unitOfWork, ITokenHelper tokenHelper, ICookieHelper cookieHelper)
    {
        _unitOfWork = Guard.Against.Null(unitOfWork, nameof(unitOfWork));
        _tokenHelper = Guard.Against.Null(tokenHelper, nameof(tokenHelper));
        _cookieHelper = Guard.Against.Null(cookieHelper, nameof(cookieHelper));
        _userManager = Guard.Against.Null(userManager, nameof(userManager));
        _signInManager = Guard.Against.Null(signInManager, nameof(signInManager));
    }

    public async Task<IBaseResponse<Token>> Handle(LoginCommand request, CancellationToken ct)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        
        if (user == null) return ResponseHelper<Token>.GetAccessDeniedErrorResponse();

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (result.Succeeded)
        {
            var newRefreshToken = await _tokenHelper.GenerateNewRefreshToken(user);
            
            user.LastLoginDate = DateTime.Now.ToUniversalTime();
            await _unitOfWork.Commit(ct);

            _cookieHelper.SetRefreshTokenInCookie(newRefreshToken);

            return new BaseResponse<Token>
            {
                StatusCode = 200,
            };
        }

        return ResponseHelper<Token>.GetAccessDeniedErrorResponse();
    }
}
