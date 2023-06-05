using Application.Common.Interfaces.Infrastructure.Repositories.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories;

public class ServiceTypeRepository : Repository<ServiceType>, IServiceTypeRepository
{
    public ServiceTypeRepository(ApplicationContext context) : base(context)
    {
    }
}