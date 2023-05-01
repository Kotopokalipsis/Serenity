using Application.Common.Interfaces.Infrastructure.Repositories.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _dbSet;

    public UserRepository(ApplicationContext context)
    {
        _dbSet = context.Set<User>();
    }

    public async Task<User> GetById(Guid guid)
    {
        return await _dbSet
            .Include(i => i.Profile)
            .FirstOrDefaultAsync(user => user.Id == guid);
    }
}