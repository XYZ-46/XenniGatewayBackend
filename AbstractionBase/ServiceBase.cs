using AbstractionBase.Interfaces;

namespace AbstractionBase
{
    public abstract class ServiceBase<TEntity>(IRepositoryBase<TEntity> repository) : IServiceBase<TEntity>
        where TEntity : BaseEntity, new()
    {

        protected readonly IRepositoryBase<TEntity> _repositoryBase = repository;

        public virtual async Task<TEntity?> GetByIdAsync(long id) => await _repositoryBase.GetByIdAsync(id);

        public virtual async Task<TEntity> AddAsync(TEntity entity) => await _repositoryBase.AddAsync(entity);

        public virtual async Task UpdateAsync(TEntity entity) => await _repositoryBase.UpdateAsync(entity);

        public virtual async Task DeleteAsync(TEntity entity) => await _repositoryBase.DeleteAsync(entity);

        public virtual async Task<IEnumerable<TEntity>> GetPagedAsync(int page, int size) => await _repositoryBase.GetPagedAsync(page, size);

    }
}
