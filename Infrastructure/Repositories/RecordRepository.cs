using Application.Common.Interfaces.Infrastructure.Repositories.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories;

public class RecordRepository : Repository<Record>, IRecordRepository
{
    public RecordRepository(ApplicationContext context) : base(context)
    {
    }
}