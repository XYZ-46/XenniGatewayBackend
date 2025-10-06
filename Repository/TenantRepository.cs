using Entities.Models;
using Infrastructure;
using Interfaces.IRepositoryCrud;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class TenantRepository(XenniDB _xenniDB) : RepositoryCrudBase<TenantModel>(_xenniDB), ITenantRepoCrud
    {
        public async Task<TenantModel?> GetByTenantNameAsync(string tenantName) => await _set.FirstOrDefaultAsync(z => z.TenantName == tenantName);
    }
}
