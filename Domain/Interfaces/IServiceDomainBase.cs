namespace Domain.Interfaces
{
    public interface IServiceDomainBase<TEntity>
    {
        Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
