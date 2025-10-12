using Infrastructure.IRepositories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TenantRepository(XenniDB _xenniDB) : RepositoryBase<TenantModel>(_xenniDB), ITenantRepo
    {
        public async Task<TenantModel?> GetByTenantNameAsync(string tenantName, CancellationToken cancellationToken = default)
            => await _set.FirstOrDefaultAsync(z => z.TenantName == tenantName && z.IsActive && !z.IsDeleted, cancellationToken: cancellationToken);
    }
}
