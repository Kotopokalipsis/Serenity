using Application.Common.Interfaces.Infrastructure.Repositories.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories;

public class TagRepository : Repository<Tag>, ITagRepository
{
    public TagRepository(ApplicationContext context) : base(context)
    {
    }
}