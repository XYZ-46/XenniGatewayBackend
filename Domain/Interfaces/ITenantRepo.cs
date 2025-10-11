using AbstractionBase.Interfaces;
using Infrastructure.Models;

namespace Domain.Interfaces
{
    public interface ITenantRepo : IRepositoryBase<TenantModel>
    {
        Task<TenantModel?> GetByTenantNameAsync(string tenantName);
    }
}
