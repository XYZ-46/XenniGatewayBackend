using Entities.Models;

namespace Interfaces.IServices
{
    public interface ITenantService : IServiceBase<TenantModel>
    {
        Task<TenantModel?> GetByTenanNameAsync(string tenantName);
        Task<TenantModel> AddUniqueTenanNameAsync(TenantModel newTenantModel);
    }
}
