using Application.Common.Interfaces.Infrastructure.Repositories.Interfaces;

namespace Application.Common.Interfaces.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    IRefreshTokenBlacklistRepository RefreshTokenBlacklistRepository { get; }
    IProfileRepository ProfileRepository { get; }
    IUserRepository UserRepository { get; }
    IMedicalCardRepository MedicalCardRepository { get; }
    IRecordRepository RecordRepository { get; }
    IServiceCategoryRepository ServiceCategoryRepository { get; }
    IServiceTypeRepository ServiceTypeRepository { get; }
    ITagRepository TagRepository { get; }
    Task Commit(CancellationToken ct = default);
}