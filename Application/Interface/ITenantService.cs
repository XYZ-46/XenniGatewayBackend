using AbstractionBase.Interfaces;
using Infrastructure.Models;

namespace Application.Interface
{
    public interface ITenantService : IServiceBase<TenantModel>
    {
        Task<TenantModel?> GetByTenanNameAsync(string tenantName);
        Task<TenantModel> AddUniqueTenanNameAsync(TenantModel newTenantModel);
    }
}
