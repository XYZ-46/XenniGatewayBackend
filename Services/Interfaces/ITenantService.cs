using Entities.Models;

namespace Services.Interfaces
{
    public interface ITenantService : IServiceBase<TenantModel>
    {
        Task<TenantModel?> GetByTenanNameAsync(string tenantName);
        Task<TenantModel> AddUniqueTenanNameAsync(TenantModel newTenantModel);
    }
}
