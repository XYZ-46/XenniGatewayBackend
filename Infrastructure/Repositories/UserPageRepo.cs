using Infrastructure.Database;
using Infrastructure.Entities;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserPageRepo : PageRepositoryBase<UserPageDto>, IUserPageRepo
    {
        public UserPageRepo(XenniDB context) : base(context)
        {
            _idKeyQuery = "usr.Id";
        }

        public override IEnumerable<UserPageDto> GetListDefault()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<UserPageDto> GetListDefault(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        // Default Order by Id or PK
        public override IQueryable<UserPageDto> GetQueryDefault()
        {
            //_baseRawQuery= GetBaseQuery();

            //_baseRawQuery = $"""
            //    {_baseRawQuery}
            //    ORDER BY usr.Id
            //    OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY
            //    """;
            return _set.FromSqlRaw(GetBaseQuery());
        }

        // Default Order by Id or PK
        public override IQueryable<UserPageDto> GetQueryDefault(int page, int pageSize)
        {
            _baseRawQuery = $"""
                {_baseRawQuery}
                ORDER BY {_idKeyQuery}
                OFFSET {(page - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY
                """;
            return _set.FromSqlRaw(_baseRawQuery);
        }

        protected override int CountItemFilter()
        {
            throw new NotImplementedException();
        }

        protected override string GetBaseQuery()
        {
            return """
                    SELECT 
                	usr.Id,
                	usr.Id UserId,
                	ucr.NickName,
                	ucr.FullName,
                	ucr.Email,
                	usr.TenantId,
                	tn.TenantName,
                	usr.IsActive,

                	usr.CreatedBy,
                	usr.CreatedDate,
                	ucr.FullName CreatedName,

                	usr.UpdatedBy,
                	usr.UpdatedDate,
                	upd.FullName UpdatedName
                FROM dbo.UserProfile usr
                LEFT JOIN dbo.Tenant tn ON tn.Id = usr.TenantId
                LEFT JOIN dbo.UserProfile ucr ON ucr.Id = usr.CreatedBy
                LEFT JOIN dbo.UserProfile upd ON upd.Id = usr.CreatedBy
                """;
        }

        protected override string GetBaseQueryFilter()
        {
            throw new NotImplementedException();
        }

        protected override string GetBaseQueryOrder()
        {
            throw new NotImplementedException();
        }

        protected override string GetBaseQueryOrderFilter()
        {
            throw new NotImplementedException();
        }
    }
}