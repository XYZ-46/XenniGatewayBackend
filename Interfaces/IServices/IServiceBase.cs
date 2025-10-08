namespace Interfaces.IServices
{
    public interface IServiceBase<TEntity>
    {
        Task<TEntity?> GetByIdAsync(Int64 id);
        Task<IEnumerable<TEntity>> GetPagedAsync(int page, int size);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
