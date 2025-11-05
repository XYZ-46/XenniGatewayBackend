namespace Infrastructure.Interface
{
    public interface IPageRepositoryBase<out TEntity>
    {

        IEnumerable<TEntity> GetListDefault();
        IEnumerable<TEntity> GetListDefault(int page, int pageSize);

        IQueryable<TEntity> GetQueryDefault();
        IQueryable<TEntity> GetQueryDefault(int page, int pageSize);
        //IQueryable<TEntity> GetQueryByRawSql();
        //IQueryable<TEntity> GetPagedQueryByRawSql(int page, int pageSize);
    }
}