using Domain.Interfaces;
using Infrastructure.IRepositories;
using Infrastructure.Models;

namespace Domain.Services
{
    public abstract class ServiceBase<TEntity>(IRepositoryBase<TEntity> repository) : IServiceBase<TEntity>
        where TEntity : BaseEntity, new()
    {

        protected readonly IRepositoryBase<TEntity> _repositoryBase = repository;

        public virtual async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default) => await _repositoryBase.GetByIdAsync(id, cancellationToken);

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default) => await _repositoryBase.AddAsync(entity, cancellationToken);

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default) => await _repositoryBase.UpdateAsync(entity, cancellationToken);

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default) => await _repositoryBase.DeleteAsync(entity, cancellationToken);

        public virtual async Task<IEnumerable<TEntity>> GetPagedAsync(int page, int size) => await _repositoryBase.GetPagedAsync(page, size);

        public Task<bool> IsExist(TEntity entity, CancellationToken canceltoken = default) => throw new NotImplementedException();
    }
}
