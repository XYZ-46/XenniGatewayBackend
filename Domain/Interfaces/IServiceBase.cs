namespace Domain.Interfaces
{
    public interface IServiceBase<TEntity>
    {
        Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetPagedAsync(int page, int size);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
