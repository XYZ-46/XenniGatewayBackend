﻿using DataTransferObject.GlobalObject;
using DataTransferObject.Tenant;
using Interfaces.IServices;
using Mapper;
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

            return new JsonResult(ApiResponseDefault<object>.Success(newTenant, "Tenant created successfully"));
        }

    }
}
