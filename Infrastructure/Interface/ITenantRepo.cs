using Infrastructure.Models;

namespace Infrastructure.Interface
{
    public interface ITenantRepo : IRepositoryBase<TenantModel>
    {
        Task<TenantModel?> GetByTenantNameAsync(string tenantName, CancellationToken cancellationToken = default);
    }
}
