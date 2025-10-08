using DataTransferObject.GlobalObject;
using Entities.Models;
using Interfaces.IRepositoryCrud;
using Interfaces.IServices;

namespace Services
{
    public class TenantService(ITenantRepoCrud tenantRepository) : ServiceBase<TenantModel>(tenantRepository), ITenantService
    {
        private readonly ITenantRepoCrud _tenantRepository = tenantRepository;

        public async Task<TenantModel?> GetByTenanNameAsync(string username) => await _tenantRepository.GetByTenantNameAsync(username);

        public async Task<TenantModel> AddUniqueTenanNameAsync(TenantModel newTenantModel)
        {
            var existingTenant = await _tenantRepository.GetByTenantNameAsync(newTenantModel.TenantName);
            if (!existingTenant.IsEmpty()) throw new XenniException($"Tenant with name '{newTenantModel.TenantName}' already exists.");

            await _tenantRepository.AddAsync(newTenantModel);
            return newTenantModel;
        }

    }
}