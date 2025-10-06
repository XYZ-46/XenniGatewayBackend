using Entities.Models;
using Interfaces.IRepositoryCrud;
using Interfaces.IServices;

namespace Services
{
    public class TenantService(ITenantRepoCrud tenantRepository) : ServiceBase<TenantModel>(tenantRepository), ITenantService
    {
        private readonly ITenantRepoCrud _tenantRepository = tenantRepository;

        public async Task<TenantModel> GetByTenanNameAsync(string username) => await _tenantRepository.GetByTenantNameAsync(username);

    }
}
