using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("UserProfile")]
    public class UserProfileModel : BaseActiveEntity
    {
        [Required]
        public long TenantId { get; set; }
        public string NickName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
