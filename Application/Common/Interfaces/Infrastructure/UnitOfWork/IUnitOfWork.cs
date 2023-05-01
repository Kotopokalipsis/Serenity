using Application.Common.Interfaces.Infrastructure.Repositories.Interfaces;

namespace Application.Common.Interfaces.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    IRefreshTokenBlacklistRepository RefreshTokenBlacklistRepository { get; }
    IProfileRepository ProfileRepository { get; }
    IUserRepository UserRepository { get; }
    Task Commit(CancellationToken ct = default);
}