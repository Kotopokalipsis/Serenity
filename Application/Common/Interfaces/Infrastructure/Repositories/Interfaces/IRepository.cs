using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Common.Interfaces.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<List<TEntity>> GetAllAsList(CancellationToken ct = default);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        bool Remove(TEntity entity);
        Task<bool> Any(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindById(Guid id);
        Task<IEnumerable<TEntity>> FindAll();
        Task<TEntity> FindOneBy(Expression<Func<TEntity, bool>> predicate);
    }
}