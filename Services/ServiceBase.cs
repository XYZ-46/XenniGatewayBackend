using Entities.Models;
using Interfaces.IRepositoryCrud;
using Interfaces.IServices;

namespace Services
{
    public abstract class ServiceBase<TEntity>(IRepositoryCrudBase<TEntity> repository) : IServiceBase<TEntity>
        where TEntity : BaseEntity, new()
    {

        protected readonly IRepositoryCrudBase<TEntity> _repositoryBase = repository;

        public virtual async Task<TEntity?> GetByIdAsync(Int64 id) => await _repositoryBase.GetByIdAsync(id) ?? new TEntity();

        public virtual async Task<TEntity> AddAsync(TEntity entity) => await _repositoryBase.AddAsync(entity);

        public virtual async Task UpdateAsync(TEntity entity) => await _repositoryBase.UpdateAsync(entity);

        public virtual async Task DeleteAsync(TEntity entity) => await _repositoryBase.DeleteAsync(entity);

        public virtual async Task<IEnumerable<TEntity>> GetPagedAsync(int page, int size) => await _repositoryBase.GetPagedAsync(page, size);

    }
}
