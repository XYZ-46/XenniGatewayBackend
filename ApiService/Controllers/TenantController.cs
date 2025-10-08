using DataTransferObject.GlobalObject;
using DataTransferObject.Tenant;
using Interfaces.IServices;
using Mapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiService.Controllers
{
    public class TenantController(ITenantService tenantService) : BaseApiController
    {
        private readonly ITenantService _tenantService = tenantService;

        //[MapToApiVersion("1")]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            Int64 idValue;
            idValue = Int64.TryParse(Id, out idValue) ? idValue : -1;
            var tenant = await _tenantService.GetByIdAsync(idValue);

            return new JsonResult(ApiResponse<object>.Found(tenant));
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {

            var (newTenantReq, tenantValidation) = await ValidateRequestAsync<AddTenantReq>();
            if (!tenantValidation.IsValid) return ValidationError(tenantValidation);

            var newTenant = newTenantReq?.MapAddTenant();
            await _tenantService.AddUniqueTenanNameAsync(newTenant);

            return new JsonResult(ApiResponse<object>.Success(newTenant, "Tenant created successfully"));
        }

    }
}
