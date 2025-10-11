using AbstractionBase;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class TenantRepository(XenniDB _xenniDB) : RepositoryBase<TenantModel>(_xenniDB), ITenantRepo
    {
        public async Task<TenantModel?> GetByTenantNameAsync(string tenantName) => await _set.FirstOrDefaultAsync(z => z.TenantName == tenantName);
    }
}
