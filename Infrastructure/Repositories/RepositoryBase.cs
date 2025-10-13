using Infrastructure.IRepositories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : BaseEntity, new()
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _set;

        protected RepositoryBase(DbContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity?> GetByIdAsync(long id, CancellationToken canceltoken = default)
            => (await _set.FindAsync([id], cancellationToken: canceltoken));

        public virtual async Task<TEntity?> GetByIdActiveAsync(long id, CancellationToken canceltoken = default)
        {
            var query = _set.Where(z => z.Id == id && !z.IsDeleted);

            // Check if the entity type supports IsActive
            if (typeof(TEntity).IsAssignableTo(typeof(BaseActiveEntity)))
            {
                query = query.Where(z => ((BaseActiveEntity)(object)z).IsActive);
            }

            return await query.FirstOrDefaultAsync(canceltoken);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken canceltoken = default)
        {
            _set.Add(entity);
            await _context.SaveChangesAsync(canceltoken);
            return entity;
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken canceltoken = default)
        {
            entity.UpdatedBy ??= 1; // TODO: Replace with actual user ID
            entity.UpdatedDate = DateTime.Now;
            _set.Update(entity);
            await _context.SaveChangesAsync(canceltoken);
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken canceltoken = default)
        {
            entity.IsDeleted = true;
            entity.UpdatedBy ??= 1; // TODO: Replace with actual user ID
            entity.UpdatedDate = DateTime.Now;
            _set.Update(entity);
            await _context.SaveChangesAsync(canceltoken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetPagedAsync(int page, int size) => await _set.OrderBy(x => x.Id).Skip((page - 1) * size).Take(size).ToListAsync();
    }
}
