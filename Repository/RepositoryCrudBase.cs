﻿using Entities.Models;
using Interfaces.IRepositoryCrud;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public abstract class RepositoryCrudBase<TEntity> : IRepositoryCrudBase<TEntity>
        where TEntity : BaseEntity, new()
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _set;

        protected RepositoryCrudBase(DbContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity?> GetByIdAsync(long id) => (await _set.FindAsync(id)) is { IsDeleted: false } entity ? entity : null;

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            _set.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            entity.UpdatedBy = entity.UpdatedBy ?? 1;
            entity.UpdatedDate = DateTime.Now;
            _set.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.UpdatedBy = entity.UpdatedBy ?? 1;
            entity.UpdatedDate = DateTime.Now;
            _set.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetPagedAsync(int page = 1, int size = 10) => await _set.Skip((page - 1) * size).Take(size).ToListAsync();
    }
}
