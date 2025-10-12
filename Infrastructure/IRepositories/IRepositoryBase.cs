namespace Infrastructure.IRepositories
{
    public interface IRepositoryBase<TEntity>
    {
        Task<TEntity?> GetByIdAsync(long id, CancellationToken canceltoken = default);
        Task<IEnumerable<TEntity>> GetPagedAsync(int page, int size);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken canceltoken = default);
        Task UpdateAsync(TEntity entity, CancellationToken canceltoken = default);
        Task DeleteAsync(TEntity entity, CancellationToken canceltoken = default);
    }
}