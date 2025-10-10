using Infrastructure;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository
{
    public class TenantRepository(XenniDB _xenniDB) : RepositoryCrudBase<TenantModel>(_xenniDB), ITenantRepo
    {
        public async Task<TenantModel?> GetByTenantNameAsync(string tenantName) => await _set.FirstOrDefaultAsync(z => z.TenantName == tenantName);
    }
}
