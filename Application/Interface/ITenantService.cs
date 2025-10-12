using Domain.Interfaces;
using Infrastructure.Models;

namespace Application.Interface
{
    public interface ITenantService : IServiceBase<TenantModel>
    {
        Task<TenantModel?> GetByTenanNameAsync(string tenantName, CancellationToken cancellationToken = default);
        Task<TenantModel> AddUniqueTenanNameAsync(TenantModel newTenantModel, CancellationToken cancellationToken = default);
    }
}
