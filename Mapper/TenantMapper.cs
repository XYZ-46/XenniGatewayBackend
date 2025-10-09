﻿using DataTransferObject.Tenant;
using Entities.Models;

namespace Mapper
{
    public static class TenantMapper
    {
        public static TenantModel MapAddTenant(this AddTenantReq addTenantReq) => new() { TenantName = addTenantReq.TenantName };
        public static TenantRes MapTenantRes(this TenantModel tenantModel)
        {
            return new()
            {
                Id = tenantModel.Id,
                TenantName = tenantModel.TenantName,
                IsActive = tenantModel.IsActive,
                CreatedDate = tenantModel.CreatedDate,
                CreatedBy = "Should be name"
            };
        }

    }
}
