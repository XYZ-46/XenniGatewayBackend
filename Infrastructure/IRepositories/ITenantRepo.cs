using Infrastructure.Models;

namespace Infrastructure.IRepositories
{
    public interface ITenantRepo : IRepositoryBase<TenantModel>
    {
        Task<TenantModel?> GetByTenantNameAsync(string tenantName, CancellationToken cancellationToken = default);
    }
}
