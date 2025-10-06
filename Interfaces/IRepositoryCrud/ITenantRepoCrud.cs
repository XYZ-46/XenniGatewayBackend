using Entities.Models;

namespace Interfaces.IRepositoryCrud
{
    public interface ITenantRepoCrud : IRepositoryCrudBase<TenantModel>
    {
        Task<TenantModel> GetByTenantNameAsync(string tenantName);
    }
}
