namespace Infrastructure.Interface
{
    public interface IRepositoryBase<TEntity>
    {
        Task<TEntity?> GetByIdAsync(long id, CancellationToken canceltoken = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken canceltoken = default);
        Task UpdateAsync(TEntity entity, CancellationToken canceltoken = default);
        Task DeleteAsync(TEntity entity, CancellationToken canceltoken = default);
    }
}