﻿namespace AbstractionBase.Interfaces
{
    public interface IRepositoryBase<TEntity>
    {
        Task<TEntity?> GetByIdAsync(long id);
        Task<IEnumerable<TEntity>> GetPagedAsync(int page, int size);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
