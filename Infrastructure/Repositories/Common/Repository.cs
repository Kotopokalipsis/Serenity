using System.Linq.Expressions;
using Application.Common.Interfaces.Infrastructure.Repositories.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Common
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationContext context)
        {
            _dbSet = context.Set<TEntity>();
        }
        
        protected virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }
        
        public virtual Task<List<TEntity>> GetAllAsList(CancellationToken ct = default)
        {
            return GetAll().ToListAsync(ct);
        }
        
        public virtual TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }
        
        public virtual TEntity Update(TEntity entity)
        {
            return _dbSet.Update(entity).Entity;
        }
        
        public virtual void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public virtual async Task<TEntity> FindById(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync();
        }
        
        public virtual async Task<IEnumerable<TEntity>> FindAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity> FindOneBy(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
        
        public virtual async Task<IEnumerable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}