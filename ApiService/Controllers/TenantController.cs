using DataTransferObject.Tenant;
using Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Mapper;

namespace ApiService.Controllers
{
    [ApiVersion("1")]
    //[ApiVersion("2")]
    [Route("v{version:apiVersion}/[controller]")]
    public class TenantController(ITenantService tenantService) : ControllerBase
    {
        private readonly ITenantService _tenantService = tenantService;

        //[MapToApiVersion("1")]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            Int64 idValue;
            idValue = Int64.TryParse(Id, out idValue) ? idValue : -1;
            var tenant = await _tenantService.GetByIdAsync(idValue);
            return Ok(tenant);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddTenantReq newTenantReq)
        {

            var newTenant = newTenantReq.MapAddTenant();
            await _tenantService.AddUniqueTenanNameAsync(newTenant);
            return Ok(newTenant);
        }

    }
}
