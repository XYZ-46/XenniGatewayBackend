using DataTransferObject.Tenant;
using Entities.Models;

namespace Mapper
{
    public static class TenantMapper
    {
        public static TenantModel MapAddTenant(this AddTenantReq addTenantReq) => new() { TenantName = addTenantReq.TenantName };

    }
}
