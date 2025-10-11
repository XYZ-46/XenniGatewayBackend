using AbstractionBase;
using Application.Interface;
using Infrastructure;
using Infrastructure.IRepositories;
using Infrastructure.Models;

namespace Application.Services
{
    public class TenantService(ITenantRepo tenantRepository) : ServiceBase<TenantModel>(tenantRepository), ITenantService
    {
        private readonly ITenantRepo _tenantRepository = tenantRepository;

        public async Task<TenantModel?> GetByTenanNameAsync(string tenantName) => await _tenantRepository.GetByTenantNameAsync(tenantName);

        public async Task<TenantModel> AddUniqueTenanNameAsync(TenantModel newTenantModel)
        {
            var existingTenant = await _tenantRepository.GetByTenantNameAsync(newTenantModel.TenantName);
            if (existingTenant is not null && !existingTenant.IsEmpty()) throw new XenniException($"Tenant with name '{newTenantModel.TenantName}' already exists.");

            await _tenantRepository.AddAsync(newTenantModel);
            return newTenantModel;
        }

    }
}