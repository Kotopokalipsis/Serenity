using Application.Common.Interfaces.Infrastructure.Repositories.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories;

public class ServiceCategoryRepository : Repository<ServiceCategory>, IServiceCategoryRepository
{
    public ServiceCategoryRepository(ApplicationContext context) : base(context)
    {
    }
}