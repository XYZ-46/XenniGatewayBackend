using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("Tenant")]
    public class TenantModel() : BaseActiveEntity
    {
        public string TenantName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
