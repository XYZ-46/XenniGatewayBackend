using ApiService.DTO.Response;
using Application.Interface;
using Application.Mapper;
using Application.RequestDTO;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ApiService.Controllers
{
    public class TenantController(ITenantService tenantService) : BaseApiController
    {
        private readonly ITenantService _tenantService = tenantService;

        [MapToApiVersion("2")]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            if (!long.TryParse(Id, out long idValue)) throw new XenniException("Data Not Found");

            var tenant = await _tenantService.GetByIdAsync(idValue);
            return tenant == null ? throw new XenniException("Data Not Found") : new JsonResult(ApiResponseDefault<object>.Found(tenant));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTenantReq _addTenantReq)
        {
            var newTenant = _addTenantReq.MapAddTenant();
            await _tenantService.AddUniqueTenanNameAsync(newTenant);

            var tenantRes = newTenant.MapTenantRes();

            return new JsonResult(ApiResponseDefault<object>.Success(tenantRes, "Tenant created successfully"));
        }

    }
}
