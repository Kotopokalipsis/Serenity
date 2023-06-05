using Application.Common.Interfaces.Application.Services;
using Application.Common.Interfaces.Infrastructure.Services;
using Application.Common.Interfaces.Infrastructure.UnitOfWork;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Services.TokenHelper;

public class TokenHelper : ITokenHelper
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtReader _jwtReader;
    private readonly UserManager<User> _userManager;
    private readonly ICookieHelper _cookieHelper;

    public TokenHelper(IJwtGenerator jwtGenerator, IUnitOfWork unitOfWork, IJwtReader jwtReader, UserManager<User> userManager, ICookieHelper cookieHelper)
    {
        _jwtGenerator = jwtGenerator;
        _unitOfWork = unitOfWork;
        _jwtReader = jwtReader;
        _userManager = userManager;
        _cookieHelper = cookieHelper;
    }

    public async Task<string> GenerateNewRefreshToken(User user)
    {
        string refreshTokenString;
            
        while (true)
        {
            refreshTokenString = _jwtGenerator.CreateRefreshToken(user.Id);
            var refreshTokenBlacklist = await _unitOfWork.RefreshTokenBlacklistRepository.FindOneBy(x => x.RefreshToken == refreshTokenString);
                
            if (refreshTokenBlacklist == null)
                break;
        }
        
        user.RefreshTokenExpirationTime = _jwtGenerator.RefreshTokenExpires().ToUniversalTime();
        
        return refreshTokenString;
    }
    
    public async Task<string> GenerateNewAccessToken(User user)
    {
        return _jwtGenerator.CreateAccessToken(user.Id);
    }
    
    public async Task<User> GetUserByRefreshToken()
    {
        var refreshToken = _cookieHelper.GetRefreshTokenFromCookie();
        
        if (string.IsNullOrEmpty(refreshToken)) return null;
        
        var blacklist = 
            await _unitOfWork.RefreshTokenBlacklistRepository
                .FindOneBy(x => x.RefreshToken == refreshToken);

        if (blacklist != null) return null;

        var claims = _jwtReader.GetClaimsFromToken(refreshToken);

        var user = 
            await _userManager.FindByIdAsync(
                claims
                    .Where(x => x.Type == "userId")
                    .Select(x => x.Value)
                    .FirstOrDefault()
            );

        if (user != null && DateTime.Now.ToUniversalTime() <= user.RefreshTokenExpirationTime)
            return DateTime.Now.ToUniversalTime() < user.RefreshTokenExpirationTime ? user : null;

        await BanRefreshToken(refreshToken);

        return null;
    }

    public async Task BanRefreshToken(string refreshToken = null)
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            refreshToken = _cookieHelper.GetRefreshTokenFromCookie();
        }

        if (string.IsNullOrEmpty(refreshToken)) return;
        
        _unitOfWork.RefreshTokenBlacklistRepository.Add(new RefreshTokenBlacklist { RefreshToken = refreshToken });
        await _unitOfWork.Commit();
    }
}