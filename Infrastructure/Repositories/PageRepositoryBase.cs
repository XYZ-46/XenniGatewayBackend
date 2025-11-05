using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public abstract class PageRepositoryBase<TEntity> : IPageRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _set;
        protected string _baseRawQuery = $"";
        protected string _idKeyQuery = string.Empty;

        protected PageRepositoryBase(DbContext context)
        {
            _context = context;
            _set = (DbSet<TEntity>)_context.Set<TEntity>().AsNoTracking();
        }

        protected abstract string GetBaseQuery();
        protected abstract string GetBaseQueryOrder();
        protected abstract string GetBaseQueryOrderFilter();
        protected abstract string GetBaseQueryFilter();
        protected abstract int CountItemFilter();

        public abstract IQueryable<TEntity> GetQueryDefault();
        public abstract IQueryable<TEntity> GetQueryDefault(int page, int pageSize);

        public abstract IEnumerable<TEntity> GetListDefault();
        public abstract IEnumerable<TEntity> GetListDefault(int page, int pageSize);


    }
}