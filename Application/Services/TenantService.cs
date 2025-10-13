using Application.Interface;
using Domain.Exception;
using Domain.Services;
using Infrastructure.IRepositories;
using Infrastructure.Models;

namespace Application.Services
{
    public class TenantService(ITenantRepo tenantRepository) : ServiceBase<TenantModel>(tenantRepository), ITenantService
    {
        private readonly ITenantRepo _tenantRepository = tenantRepository;

        public async Task<TenantModel?> GetByTenanNameAsync(string tenantName, CancellationToken cancellationToken = default) => await _tenantRepository.GetByTenantNameAsync(tenantName, cancellationToken);

        public async Task<TenantModel> AddUniqueTenanNameAsync(TenantModel newTenantModel, CancellationToken cancellationToken = default)
        {
            var existingTenant = await _tenantRepository.GetByTenantNameAsync(newTenantModel.TenantName, cancellationToken);
            if (existingTenant is not null && !existingTenant.IsEmpty()) throw new XenniException($"Tenant with name '{newTenantModel.TenantName}' already exists.");

            await _tenantRepository.AddAsync(newTenantModel, cancellationToken);
            return newTenantModel;
        }

    }
}