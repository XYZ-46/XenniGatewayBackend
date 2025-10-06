using Entities.Models;
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

        public virtual async Task<TEntity> GetByIdAsync(Int64 id)
        {
            var entity = await _set.FindAsync(id);
            if (entity?.Id > 0 && !entity.IsDeleted) return entity;
            else return new TEntity();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            _set.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _set.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;
            _set.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetPagedAsync(int page, int size) => await _set.Skip((page - 1) * size).Take(size).ToListAsync();

    }
}
