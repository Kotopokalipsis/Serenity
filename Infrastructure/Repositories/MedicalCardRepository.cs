using Application.Common.Interfaces.Infrastructure.Repositories.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories;

public class MedicalCardRepository : Repository<MedicalCard>, IMedicalCardRepository
{
    public MedicalCardRepository(ApplicationContext context) : base(context)
    {
    }
}