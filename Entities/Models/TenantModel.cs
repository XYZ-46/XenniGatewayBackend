using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Tenant")]
    public class TenantModel() : BaseEntity
    {
        public string TenantName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
