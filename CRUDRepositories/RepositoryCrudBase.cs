using BaseAbstraction;
using Entities.Interfaces;

namespace CrudRepositories
{
    public abstract class RepositoryCrudBase<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        public Task<TEntity?> GetByIdAsync(Int64 id)
        {
            throw new NotImplementedException();
        }
        public Task<TEntity> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(Int64 id)
        {
            throw new NotImplementedException();
        }
    }
}
