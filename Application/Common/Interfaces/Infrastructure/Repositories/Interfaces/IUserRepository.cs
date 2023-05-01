using Domain.Entities;

namespace Application.Common.Interfaces.Infrastructure.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> GetById(Guid guid);
}