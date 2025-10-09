using Entities.Models;

namespace Repository.Interfaces
{
    public interface ITenantRepo : IRepositoryBase<TenantModel>
    {
        Task<TenantModel?> GetByTenantNameAsync(string tenantName);
    }
}
